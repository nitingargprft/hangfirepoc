using Integration.MSMQ.Helper;
using Integration.MSMQ.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Integration.MSMQ.Helper.Enums;

namespace Integration.MSMQ
{
    public class MessageQueueHandler : IMessageQueueHandler
    {
        #region Public Methods

        public virtual MessageQueueClient ConnectToQueue(string machineName, string queueName)
        {
            MessageQueueClient messageQueueClient = null;
            try
            {
                MessageQueue[] messageQueues = this.GetPrivateQueuesByMachine(machineName);
                if (messageQueues != null && messageQueues.Length > 0)
                {
                    MessageQueue messageQueue = messageQueues.FirstOrDefault(o => o.QueueName.Equals(queueName, StringComparison.OrdinalIgnoreCase));
                    if (messageQueue != null)
                    {
                        messageQueueClient = new MessageQueueClient(messageQueue, machineName, queueName);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return messageQueueClient;
        }

        public object ReadMessage(MessageQueueClient messageQueueClient)
        {
            List<string> messages = new List<string>();
            // Create a transaction because we are using a transactional queue.
            using (var messageQueueTransaction = new MessageQueueTransaction())
            {
                try
                {
                    // Create queue object
                    using (var messageQueue = messageQueueClient.MessageQueue)
                    {
                        messageQueue.Formatter = (IMessageFormatter)new BinaryMessageFormatter();
                        messageQueueTransaction.Begin();
                        Message[] messagearray = messageQueue.GetAllMessages();
                        foreach (Message message in messagearray)
                        {
                            if (message != null && message.Body != null)
                            {
                                object messageBody = message.Body;
                                if (messageBody != null)
                                {
                                    messages.Add(messageBody.ToString());
                                    messageQueue.ReceiveById(message.Id);
                                }
                            }
                        }
                        messageQueueTransaction.Commit();
                    }
                }
                catch
                {
                    messageQueueTransaction.Abort(); // rollback the transaction
                }
            }

            return messages;
        }

        public virtual void SendMessageToQueue<T>(MessageQueueClient messageQueueClient, T message, string label, MessageFormatter messageFormatter)
        {
            // Create a transaction because we are using a transactional queue.
            using (var messageQueueTransaction = new MessageQueueTransaction())
            {
                try
                {
                    // Create queue object
                    using (var messageQueue = messageQueueClient.MessageQueue)
                    {
                        if (messageFormatter.Equals(Enums.MessageFormatter.JsonMessageFormatter))
                            messageQueue.Formatter = new JsonMessageFormatter();
                        else if (messageFormatter.Equals(Enums.MessageFormatter.BinaryFormatter))
                            messageQueue.Formatter = (IMessageFormatter)new BinaryMessageFormatter();
                        else if (messageFormatter.Equals(Enums.MessageFormatter.XmlMessageFormatter))
                            messageQueue.Formatter = new XmlMessageFormatter();

                        messageQueue.DefaultPropertiesToSend.Recoverable = true;

                        // push message onto queue (inside of a transaction)
                        messageQueueTransaction.Begin();
                        messageQueue.Send(message, label, messageQueueTransaction);
                        messageQueueTransaction.Commit();
                    }
                }
                catch
                {
                    messageQueueTransaction.Abort(); // rollback the transaction
                }
            }
        }

        #endregion

        #region Private Methods

        private MessageQueue[] GetPrivateQueuesByMachine(string machineName)
        {
            try
            {
                MessageQueue[] privateQueuesByMachine = MessageQueue.GetPrivateQueuesByMachine(machineName);
                return privateQueuesByMachine;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion


    }
}
