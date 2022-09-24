using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Integration.MSMQ.Helper
{
    public class MessageQueueClient
    {
        public MessageQueue MessageQueue { get; private set; }
        public string QueueName { get; set; }
        public string MachineName { get; set; }

        public MessageQueueClient(MessageQueue MessageQueue, string QueueName, string MachineName)
        {
            this.MessageQueue = MessageQueue;
            this.QueueName = QueueName;
            this.MachineName = MachineName;
        }
    }


}
