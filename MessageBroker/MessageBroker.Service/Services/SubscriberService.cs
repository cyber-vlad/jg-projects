using MessageBroker.Common.Entities;
using MessageBroker.Common.Interfaces;
using MessageBroker.Common.Models;
using System;
using System.Messaging;
using System.ServiceModel;

namespace MessageBroker.Service.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SubscriberService : ISubscriber
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        
        public SubscriberService() { }

        public SubscriberService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public SubscriptionResponse Subscribe(Subscriber subscriber, string topic)
        {
            var existingSubscription = _subscriptionRepository.GetSubscription(subscriber.Id, topic);

            if (existingSubscription != null)
            {
                return new SubscriptionResponse
                {
                    Topic = topic,
                    QueuePath = existingSubscription.QueuePath
                };
            }

            var queuePath = $@"{MsmqConfig.MsmqBasePath}{topic}_{subscriber.Id}";

            var subscription = new Subscription
            {
                SubscriberId = subscriber.Id,
                Topic = topic,
                QueuePath = queuePath
            };

            _subscriptionRepository.Add(subscription);

            return new SubscriptionResponse
            {
                Topic = topic,
                QueuePath = queuePath
            };
        }
    }
}