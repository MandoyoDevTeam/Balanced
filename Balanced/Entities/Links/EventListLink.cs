using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class EventListLink
    {
        [JsonProperty("events.callbacks")]
        public string Callbacks { get; set; }

    }
}
