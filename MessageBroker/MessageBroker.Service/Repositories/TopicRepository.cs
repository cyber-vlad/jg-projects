using MessageBroker.Common.Entities;
using MessageBroker.Common.Enums;
using MessageBroker.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageBroker.Service.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private static readonly List<Topic> _topics = new List<Topic>();

        public TopicRepository()
        {
            _topics.Add(new Topic { Name = "wcf", Type = TopicType.Technology, QueuePaths = new List<string> { "queue1", "queue2" }, Descriptions = new List<string> { "W", "C", "F" }, Tags = new Dictionary<string, string> { { "env", "prod" }, { "owner", "teamA" }}, Metadata = new Tuple<int, string>(1, "W") });
            _topics.Add(new Topic { Name = "money", Type = TopicType.Business, QueuePaths = new List<string> { "queue3", "queue4" }, Descriptions = new List<string> { "M", "O", "N", "E", "Y" }, Tags = new Dictionary<string, string> { { "none", "dev" }, { "deputy", "teamB" } }, Metadata = new Tuple<int, string>(1, "M")});
        }

        public void Add(Topic topic)
        {
            _topics.Add(topic);
        }

        public void DeleteByName(string topicName)
        {
            _topics.RemoveAll(t => t.Name == topicName);
        }

        public bool Exists(string topicName)
        {
            return _topics.Any(t => t.Name == topicName);
        }

        public ICollection<Topic> GetAllTopics()
        {
            return _topics.ToList();
        }

        public Topic GetTopicByName(string topicName)
        {
            return _topics.FirstOrDefault(t => t.Name == topicName);
        }
    }
}
