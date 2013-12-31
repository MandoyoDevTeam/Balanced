    using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{
    [DataContract]
    public enum  SecurityCodeStatus
    {
        [JsonProperty("unknown")]
        Unknown = 0,

        [JsonProperty("passed")]
        Passed = 1,

        [JsonProperty("failed")]
        Failed = 2,

    }
}