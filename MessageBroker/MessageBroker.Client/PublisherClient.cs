using MessageBroker.Common.Consts;
using MessageBroker.Common.Entities;
using MessageBroker.Common.Interfaces;
using System;
using System.ServiceModel;

namespace MessageBroker.Client
{
    internal class PublisherClient
    {
        private readonly IPublisher _proxy;
        
        public PublisherClient()
        {
            var binding = new WSHttpBinding();
            var endpoint = new EndpointAddress(BrokerConfig.PublisherBaseUrl + BrokerConfig.PublisherServiceName);

            var channelFactory = new ChannelFactory<IPublisher>(binding, endpoint);
            _proxy = channelFactory.CreateChannel();
        }

        public void PublishMessage(Message message)
        {
            try
            {
                _proxy.Publish(message);
                Console.WriteLine($"[Published]: {message.Topic} | {message.Content}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"[Error] [publishing]: {ex.Message}");
            }
        }
    }
}
