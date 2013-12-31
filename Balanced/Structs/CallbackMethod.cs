
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{
    [DataContract]
    public enum CallbackMethod
    {
        [JsonProperty("unknown")]
        Unknown = 0,

        [JsonProperty("post")]
        Post = 1,
        
        [JsonProperty("put")]
        Put = 2,

        [JsonProperty("get")]
        Get = 3,

    }
}
