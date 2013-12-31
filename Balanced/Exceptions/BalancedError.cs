using System.Collections.Generic;
using System.Runtime.Serialization;
using Balanced.Entities;
using Balanced.Structs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Balanced.Exceptions
{
    [DataContract]
    public class BalancedError : BalancedObject
    {
        [JsonProperty("status_code")]
        public StatusCode StatusCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("additional")]
        public string Additional { get; set; }

        [JsonProperty("category_type")]
        public string CategoryType { get; set; }

        [JsonProperty("category_code")]
        public string CategoryCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("extras")]
        public Dictionary<string, object> Extras { get; set; }
    }
}
