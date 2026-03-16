using MessageBroker.Common.Entities;
using System;
using System.Threading.Tasks;

namespace MessageBroker.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(">> Enter type (publisher [p] / subscriber [s])");
            string type = Console.ReadLine();
            Console.Clear();

            if(type == "s")
            {
                RunSubscriber();
            }
            else if(type == "p")
            {
                RunPublisher();
            }
        }

        private static void RunSubscriber()
        {
            Console.WriteLine(">> Enter ID: ");
            int subscriberId = int.Parse(Console.ReadLine());

            var subscriberInfo = new Subscriber { Id = subscriberId };
            var subscriber = new SubscriberClient(subscriberInfo);
            
            Console.WriteLine(">> Enter topic: ");
            var topic = Console.ReadLine();

            var response = subscriber.Subscribe(topic);

            Task.Run(() => subscriber.ListenMessages(response.QueuePath));

            Console.ReadLine();
        }

        private static void RunPublisher()
        {
            Console.WriteLine(">> Enter topic: ");
            var topic = Console.ReadLine();

            var publisher = new PublisherClient();

            while (true)
            {
                Console.WriteLine(">> Enter message: ");
                var content = Console.ReadLine();
                
                var message = new Message
                {
                    Topic = topic,
                    Content = content
                };

                publisher.PublishMessage(message);
            }
        }
    }
}
