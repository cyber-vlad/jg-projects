using MessageBroker.Client.ServiceReference1;
using MessageBroker.Client.ServiceReference2;
using MessageBroker.Client.ServiceReference3;
using MessageBroker.Common.Entities;
using MessageBroker.Common.Enums;
using MessageBroker.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Messaging;
using System.Threading.Tasks;
using Message = MessageBroker.Common.Entities.Message;

namespace MessageBroker.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(">> Enter type (publisher [p] / subscriber [s] / admin [a]");
            string type = Console.ReadLine();
            Console.Clear();

            switch(type)
            {
                case "p": RunPublisher(); break;
                case "s": RunSubscriber(); break;
                case "a": RunAdmin(); break;
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

        private static void RunAdmin()
        {
            var client = new TopicClient();

            while(true)
            {
                Console.WriteLine("[1] Display all topis");
                Console.WriteLine("[2] Display a topic");
                Console.WriteLine("[3] Add a topic");
                Console.WriteLine("[4] Delete a topic");

                Console.Write(">> Enter an option: ");
                string option = Console.ReadLine();

                switch(option)
                {
                    case "1":
                        var topics = client.GetExistingTopics();
                        foreach(var t in topics)
                            Console.WriteLine(t.Name);
                        foreach(var t  in topics)
                            t.Show();
                        break;
                    case "2":
                        Console.Write(">> Enter name: ");
                        var name = Console.ReadLine();
                        client.GetTopicByName(name).Show();
                        break;
                    case "3":
                        var response = client.Add(new Topic { Name = "jg", Type = TopicType.Business, QueuePaths = new List<string> { "queue3", "queue4" }, Descriptions = new List<string> { "J", "G" }, Tags = new Dictionary<string, string> { { "none", "dev" }, { "deputy", "teamB" } }, Metadata = new Tuple<int, string>(1, "J") });
                        Console.WriteLine(response.Description);
                        break;
                    case "4":
                        Console.Write(">> Enter name: ");
                        var nameTopic = Console.ReadLine();
                        var resp = client.DeleteByName(nameTopic);
                        Console.WriteLine(resp.Description);
                        break;

                }
            }

            


        }
    }
}
