using MessageBroker.Common.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MessageBroker.Common.Entities
{
    [DataContract]
    public class Topic : BaseEntity
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public TopicType Type { get; set; }

        [DataMember] public ICollection<string> QueuePaths { get; set; }

        [DataMember] public List<string> Descriptions { get; set; }
        [DataMember] public Dictionary<string, string> Tags { get; set; }
        [DataMember] public Tuple<int, string> Metadata { get; set; }
    }
}
