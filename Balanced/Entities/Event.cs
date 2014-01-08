using System.Runtime.Serialization;
using Balanced.Helpers;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Event : BalancedObject
    {
        [JsonProperty("callback_statuses")]
        public CallbackStatuses CallbackStatuses { get; set; }

        [JsonProperty("entity")]
        [JsonConverter(typeof(EntitySerializer))]
        public BalancedList Entity { get; set; }

        [JsonProperty("occurred_at")]
        public string OccurredAt { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("links")]
        public EventLink Links { get; set; }
    }
}
