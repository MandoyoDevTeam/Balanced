using System.Collections.Generic;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    public abstract class BalancedObject
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

    }
}
