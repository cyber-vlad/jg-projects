using MessageBroker.Common.Entities;
using MessageBroker.Common.Models;
using System.ServiceModel;

namespace MessageBroker.Common.Interfaces
{
    [ServiceContract]
    public interface ISubscriber
    {
        [OperationContract]
        SubscriptionResponse Subscribe(Subscriber subscriber, string topic);
    }
}
