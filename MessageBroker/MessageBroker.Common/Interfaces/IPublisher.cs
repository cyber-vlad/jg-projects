using MessageBroker.Common.Entities;
using System.ServiceModel;

namespace MessageBroker.Common.Interfaces
{
    [ServiceContract]
    public interface IPublisher
    {
        [OperationContract(IsOneWay = true)]
        void Publish(Message message);
    }
}
