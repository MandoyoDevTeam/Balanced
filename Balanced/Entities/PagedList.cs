using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class PagedList
    {

        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
        
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

       [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }
    }
}
