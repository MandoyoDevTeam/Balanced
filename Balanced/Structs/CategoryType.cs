using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{

    [DataContract]
    public enum CategoryType
    {
        [JsonProperty("unknown")]
        Unknown = 0,

        [JsonProperty("request")]
        Checking = 1,

        [JsonProperty("banking")]
        Banking = 2,

        [JsonProperty("logical")]
        Logical = 3
    }
}
