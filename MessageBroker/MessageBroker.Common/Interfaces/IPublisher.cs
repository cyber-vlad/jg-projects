using MessageBroker.Common.Entities;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MessageBroker.Common.Interfaces
{
    [ServiceContract]
    public interface IPublisher
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void Publish(Message message);
    }
}
