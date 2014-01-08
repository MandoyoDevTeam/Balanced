using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class CreditListLink
    {
        [JsonProperty("credits.customer")]
        public string Customer { get; set; }

        [JsonProperty("credits.destination")]
        public string Destination { get; set; }

        [JsonProperty("credits.events")]
        public string Events { get; set; }

        [JsonProperty("credits.order")]
        public string Order { get; set; }

        [JsonProperty("credits.reversals")]
        public string Reversals { get; set; }

    }
}
