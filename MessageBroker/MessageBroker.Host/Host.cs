using MessageBroker.Common.Consts;
using MessageBroker.Common.Interfaces;
using MessageBroker.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace MessageBroker.Host
{
    internal class Host
    {
        private ServiceHost _publisherHost;
        private ServiceHost _subscriberHost;

        private readonly IServiceProvider _serviceProvider;

        public Host(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            StartPublisher();
            StartSubscriber();
        }

        public void Stop()
        {
            _publisherHost.Close();
            _subscriberHost.Close();
        }

        private void StartPublisher()
        {
            var service = _serviceProvider.GetRequiredService<PublisherService>();
            Uri baseAddress = new Uri(BrokerConfig.PublisherBaseUrl);

            _publisherHost = new ServiceHost(service, baseAddress);
            _publisherHost.AddServiceEndpoint(typeof(IPublisher), new WSHttpBinding(), BrokerConfig.PublisherServiceName);

            var smb = new ServiceMetadataBehavior()
            {
                HttpGetEnabled = true
            };

            _publisherHost.Description.Behaviors.Add(smb);
            _publisherHost.Open();

            Console.WriteLine("[Publisher] Running");
        }
        
        private void StartSubscriber()
        {
            var service = _serviceProvider.GetRequiredService<SubscriberService>();
            Uri baseAddress = new Uri(BrokerConfig.SubscriberBaseUrl);

            _subscriberHost = new ServiceHost(service, baseAddress);
            _subscriberHost.AddServiceEndpoint(typeof(ISubscriber), new WSHttpBinding(), BrokerConfig.SubscriberServiceName);

            var smb = new ServiceMetadataBehavior()
            {
                HttpGetEnabled = true
            };

            _subscriberHost.Description.Behaviors.Add(smb);
            _subscriberHost.Open();

            Console.WriteLine("[Subscriber] Running");
        }
    }
}
