using System;
using System.Runtime.Serialization;

namespace MessageBroker.Common.Models
{
    [Serializable]
    [DataContract]
    public class SubscriptionResponse
    {
        [DataMember]
        public string Topic { get; set; }
        [DataMember]
        public string QueuePath { get; set; }
    }
}
