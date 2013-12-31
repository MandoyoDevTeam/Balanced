using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Callback : BalancedObject
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("method")]
        public CallbackMethod Method { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }
    }
}
