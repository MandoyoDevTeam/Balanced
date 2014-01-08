using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class DebitListLink
    {
        [JsonProperty("debits.customer")]
        public string Customer { get; set; }

        [JsonProperty("debits.events")]
        public string Events { get; set; }

        [JsonProperty("debits.order")]
        public string Order { get; set; }

        [JsonProperty("debits.refunds")]
        public string Refunds { get; set; }

        [JsonProperty("debits.source")]
        public string Source { get; set; }

    }
}
