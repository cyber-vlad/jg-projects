using MessageBroker.Common.Entities;
using MessageBroker.Common.Interfaces;
using MessageBroker.Service.Infrastructure;
using System.ServiceModel;

namespace MessageBroker.Service.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PublisherService : IPublisher
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public PublisherService() { }

        public PublisherService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public void Publish(Message message)
        {
            var subscriptions = _subscriptionRepository.GetSubscriptionsByTopic(message.Topic);

            foreach (var subscription in subscriptions)
            {
                MsmqProvider.SendMessage(subscription.QueuePath, message);
            }
        }
    }
}