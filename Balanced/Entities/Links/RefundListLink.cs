using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class RefundListLink
    {
        [JsonProperty("refunds.debit")]
        public string Debit { get; set; }

        [JsonProperty("refunds.events")]
        public string Events { get; set; }

        [JsonProperty("refunds.order")]
        public string Order { get; set; }

    }
}
