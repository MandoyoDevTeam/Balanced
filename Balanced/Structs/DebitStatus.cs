using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{

    [DataContract]
    public enum DebitStatus
    {
        [JsonProperty("unknown")]
        Unknown = 0,

        [JsonProperty("failed")]
        Failed = 1,

        [JsonProperty("pending")]
        Pending = 2,

        [JsonProperty("retrying")]
        Retrying = 3,

        [JsonProperty("succeeded")]
        Succeeded = 4

    }
}
