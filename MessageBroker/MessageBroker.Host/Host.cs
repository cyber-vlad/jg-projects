using MessageBroker.Common.Interfaces;
using MessageBroker.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceModel;

namespace MessageBroker.Host
{
    internal class Host
    {
        private ServiceHost _publisherHost;
        private ServiceHost _subscriberHost;
        private ServiceHost _topicHost;

        private readonly IServiceProvider _serviceProvider;

        public Host(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            StartPublisher();
            StartSubscriber();
            StartTopic();
        }

        public void Stop()
        {
            _publisherHost.Close();
            _subscriberHost.Close();
            _topicHost.Close();
        }

        private void StartPublisher()
        {
            var service = _serviceProvider.GetRequiredService<PublisherService>();

            _publisherHost = new ServiceHost(service);

            _publisherHost.Open();

            Console.WriteLine("[Publisher] Running");
        }
        
        private void StartSubscriber()
        {
            var service = _serviceProvider.GetRequiredService<SubscriberService>();

            _subscriberHost = new ServiceHost(service);

            _subscriberHost.Open();

            Console.WriteLine("[Subscriber] Running");
        }

        private void StartTopic()
        {
            var service = _serviceProvider.GetRequiredService<TopicService>();

            _topicHost = new ServiceHost(service);

            _topicHost.Open();

            Console.WriteLine("[Topic] Running");
        }
    }
}
