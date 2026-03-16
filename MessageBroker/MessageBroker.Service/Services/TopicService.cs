using MessageBroker.Common.Entities;
using MessageBroker.Common.Enums;
using MessageBroker.Common.Interfaces;
using MessageBroker.Common.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace MessageBroker.Service.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TopicService : ITopic
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService() { }

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public BaseResponse Add(Topic topic)
        {
            if(!_topicRepository.Exists(topic.Name))
            {
                _topicRepository.Add(topic);
                return new BaseResponse { StatusCode = StatusCode.Success, Description = $"Topic {topic.Name} added successfully [{topic.CreatedAt}]" };
            }

            return new BaseResponse { StatusCode = StatusCode.Fail, Description = $"Topic {topic.Name} already created" };
        }

        public BaseResponse DeleteByName(string topicName)
        {
            if (_topicRepository.Exists(topicName))
            {
                _topicRepository.DeleteByName(topicName);
                return new BaseResponse { StatusCode = StatusCode.Success, Description = $"Topic {topicName} deleted successfully [{DateTime.UtcNow}]" };
            }

            return new BaseResponse { StatusCode = StatusCode.Fail, Description = $"Topic {topicName} doesn't exist" };
        }

        public ICollection<Topic> GetExistingTopics()
        {
            return _topicRepository.GetAllTopics();
        }

        public Topic GetTopicByName(string topicName)
        {
            return _topicRepository.GetTopicByName(topicName);
        }
    }
}
