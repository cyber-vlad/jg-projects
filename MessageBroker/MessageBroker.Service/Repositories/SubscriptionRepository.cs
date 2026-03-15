using MessageBroker.Common.Entities;
using MessageBroker.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MessageBroker.Service.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private static readonly List<Subscription> _subscriptions = new List<Subscription>();

        public void Add(Subscription subscription)
        {
            if(!_subscriptions.Any(s => s.SubscriberId == subscription.SubscriberId && s.Topic == subscription.Topic))
                _subscriptions.Add(subscription);
        }

        public void Delete(int subscriberId, string topic)
        {
            _subscriptions.RemoveAll(s => s.SubscriberId == subscriberId && s.Topic == topic);
        }

        public List<Subscription> GetSubscriptionsByTopic(string topic)
        {
            return _subscriptions.Where(s => s.Topic == topic).ToList();
        }

        public Subscription GetSubscription(int subscriberId, string topic)
        {
            return _subscriptions.FirstOrDefault(s => s.SubscriberId == subscriberId && s.Topic == topic);
        }
    }
}
