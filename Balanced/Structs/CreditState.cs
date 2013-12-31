using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{
    [DataContract]
    public enum CreditState
    {
        [JsonProperty("unknown")]
        Unknown = 0,

        [JsonProperty("pending")]
        Pending = 1,

        [JsonProperty("cleared")]
        Cleared = 2,

        [JsonProperty("rejected")]
        Rejected = 3,
        
        
    }
}
