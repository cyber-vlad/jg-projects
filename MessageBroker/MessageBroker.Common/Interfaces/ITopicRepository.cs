using MessageBroker.Common.Entities;
using System.Collections.Generic;

namespace MessageBroker.Common.Interfaces
{
    public interface ITopicRepository
    {
        void Add(Topic topic);
        void DeleteByName(string topicName);
        Topic GetTopicByName(string topicName);
        ICollection<Topic> GetAllTopics();
        bool Exists(string topicName);
    }
}
