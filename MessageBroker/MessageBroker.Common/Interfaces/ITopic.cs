using MessageBroker.Common.Entities;
using MessageBroker.Common.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace MessageBroker.Common.Interfaces
{
    [ServiceContract]
    public interface ITopic
    {
        [OperationContract]
        Topic GetTopicByName(string topicName);

        [OperationContract]
        ICollection<Topic> GetExistingTopics();

        [OperationContract]
        BaseResponse Add(Topic topic);

        [OperationContract]
        BaseResponse DeleteByName(string topicName);
    }
}
