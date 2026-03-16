using MessageBroker.Client.ServiceReference1;
using MessageBroker.Client.ServiceReference2;
using MessageBroker.Common.Entities;
using System;
using System.Messaging;
using System.Threading.Tasks;
using Message = MessageBroker.Common.Entities.Message;

namespace MessageBroker.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(">> Enter type (publisher [p] / subscriber [s])");
            string type = Console.ReadLine();
            Console.Clear();

            switch(type)
            {
                case "p": RunPublisher(); break;
                case "s": RunSubscriber(); break;
                default: Console.WriteLine("Bye"); break;  
            }
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

                publisher.Publish(message);
            }
        }

        private static void RunSubscriber()
        {
            Console.WriteLine(">> Enter ID: ");
            int subscriberId = int.Parse(Console.ReadLine());

            var subscriberInfo = new Subscriber { Id = subscriberId };
            var subscriber = new SubscriberClient();

            Console.WriteLine(">> Enter topic: ");
            var topic = Console.ReadLine();

            var response = subscriber.Subscribe(subscriberInfo, topic);

            Task.Run(() => ListenMessages(response.QueuePath));

            Console.ReadLine();
        }

        private static void ListenMessages(string queuePath)
        {
            if (!MessageQueue.Exists(queuePath))
            {
                Console.WriteLine("[Error] Queue does not exist");
                return;
            }

            var queue = new MessageQueue(queuePath);
            queue.Formatter = new BinaryMessageFormatter();

            queue.ReceiveCompleted += (sender, e) =>
            {
                var q = (MessageQueue)sender;
                q.BeginReceive();
                try
                {
                    var msg = q.EndReceive(e.AsyncResult);
                    var message = (Message)msg.Body;
                    Console.WriteLine($"[Listening]: {message.Topic} | {message.Content}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error][Listening] : {ex.Message}");
                }
                finally
                {
                    queue.BeginReceive();
                }
            };

            queue.BeginReceive();
        }
    }
}
