using System;
using System.Runtime.Serialization;

namespace MessageBroker.Common.Entities
{
    [DataContract]
    public class BaseEntity : IAuditableEntity
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
    }
}
