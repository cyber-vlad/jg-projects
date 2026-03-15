using System;
using System.Runtime.Serialization;

namespace MessageBroker.Common.Entities
{
    [Serializable]
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Topic { get; set; }
        [DataMember]
        public string Content { get; set; }
    }
}