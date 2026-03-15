using MessageBroker.Common.Consts;
using MessageBroker.Common.Entities;
using MessageBroker.Common.Interfaces;
using MessageBroker.Common.Models;
using System;
using System.Messaging;
using System.ServiceModel;

namespace MessageBroker.Client
{
    internal class SubscriberClient
    {
        private readonly ISubscriber _proxy;
        private readonly Subscriber _subscriber;

        public SubscriberClient(Subscriber subscriber)
        {
            _subscriber = subscriber;

            var binding = new WSHttpBinding();
            var endpoint = new EndpointAddress(BrokerConfig.SubscriberBaseUrl + BrokerConfig.SubscriberServiceName);

            var channelFactory = new ChannelFactory<ISubscriber>(binding, endpoint);
            _proxy = channelFactory.CreateChannel();
        }

        public SubscriptionResponse Subscribe(string topic)
        {
            try
            {
                var response = _proxy.Subscribe(_subscriber, topic);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error][Subscribing]: {ex.Message}");
                throw new Exception("Subscription failed");
            }
        }

        public void ListenMessages(string queuePath)
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
                try
                {
                    var msmqMessage = queue.EndReceive(e.AsyncResult);
                    var message = (Common.Entities.Message)msmqMessage.Body;

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
