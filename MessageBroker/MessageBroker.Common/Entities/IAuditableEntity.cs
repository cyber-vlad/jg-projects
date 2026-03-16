using System;
using System.Runtime.Serialization;

namespace MessageBroker.Common.Entities
{
    [DataContract]
    public class IAuditableEntity
    {
        [DataMember] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [DataMember] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
