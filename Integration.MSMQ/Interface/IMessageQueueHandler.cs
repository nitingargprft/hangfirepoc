using Integration.MSMQ.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Integration.MSMQ.Helper.Enums;

namespace Integration.MSMQ.Interface
{
    public interface IMessageQueueHandler
    {
        MessageQueueClient ConnectToQueue(string machineName, string queueName);
        void SendMessageToQueue<T>(MessageQueueClient messageQueueClient, T message, string label, MessageFormatter messageFormatter);

        object ReadMessage(MessageQueueClient messageQueueClient);
    }
}
