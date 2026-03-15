using System.Runtime.Serialization;

namespace MessageBroker.Common.Entities
{
    [DataContract]
    public class Subscriber
    {
        [DataMember]
        public int Id { get; set; }
    }
}
