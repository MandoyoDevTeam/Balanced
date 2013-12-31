
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{
    [DataContract]
    public enum CreditStatus
    {
        [JsonProperty("unknown")]
        Unknown = 0,

        [JsonProperty("paid")]
        Paid = 1,
        
        [JsonProperty("pending")]
        Pending = 2,

        [JsonProperty("succeeded")]
        Succeeded = 3,

        [JsonProperty("failed")]
        Failed = 4,
    }
}
