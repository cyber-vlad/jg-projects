using MessageBroker.Common.Enums;
using System.Runtime.Serialization;

namespace MessageBroker.Common.Models
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember] public StatusCode StatusCode { get; set; }
        
        [DataMember] public string Description { get; set; }
    }
}
