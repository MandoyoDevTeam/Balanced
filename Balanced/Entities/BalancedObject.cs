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

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("created_at")]
        public string CreatedOn { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}
