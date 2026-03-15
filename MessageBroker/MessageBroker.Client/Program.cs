using MessageBroker.Common.Entities;
using System;
using System.Threading.Tasks;

namespace MessageBroker.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var subscriberInfo = new Subscriber
            {
                Id = 1,
            };

            var publisher = new PublisherClient();
            var subscriber = new SubscriberClient(subscriberInfo);

            var topic = "wcf";

            var response = subscriber.Subscribe(topic);

            Task.Run(() => subscriber.ListenMessages(response.QueuePath));

            var message = new Message
            {
                Topic = topic,
                Content = "Using WCF + MSMQ"
            };

            publisher.PublishMessage(message);

            Console.ReadLine();
        }
    }
}
