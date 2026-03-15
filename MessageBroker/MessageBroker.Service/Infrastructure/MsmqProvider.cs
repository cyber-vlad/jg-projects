using System;
using System.Messaging;

namespace MessageBroker.Service.Infrastructure
{
    public static class MsmqProvider
    {
        public static void SendMessage(string queuePath, Common.Entities.Message message)
        {
            if (!MessageQueue.Exists(queuePath))
                MessageQueue.Create(queuePath, false);

            using (var queue = new MessageQueue(queuePath))
            {
                queue.Formatter = new BinaryMessageFormatter();
                queue.Send(message, MessageQueueTransactionType.None);
            }
        }
    }
}
