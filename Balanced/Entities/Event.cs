using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Event : BalancedObject
    {
        [JsonProperty("callback_statuses")]
        public CallbackStatuses CallbackStatuses { get; set; }

        [JsonProperty("entity")]
        public Verification Entity { get; set; }

        [JsonProperty("callbacks_uri")]
        public string CallbacksUri { get; set; }

        [JsonProperty("occurred_at")]
        public string OccurredAt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }    
    }
}
