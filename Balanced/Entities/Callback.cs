using System.Collections.Generic;
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

        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        public CallbackMethod Method { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("links")]
        public Dictionary<string,string> Links { get; set; }
    }
}
