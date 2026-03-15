using MessageBroker.Common.Entities;
using System.Collections.Generic;

namespace MessageBroker.Common.Interfaces
{
    public interface ISubscriptionRepository
    {
        void Add(Subscription subscription);
        void Delete(int subscriberId, string topic);
        List<Subscription> GetSubscriptionsByTopic(string topic);
        Subscription GetSubscription(int subscriberId, string topic);
    }
}
