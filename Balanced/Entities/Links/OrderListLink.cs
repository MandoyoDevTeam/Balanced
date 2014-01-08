using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class OrderListLink
    {
        [JsonProperty("orders.buyers")]
        public string Buyers { get; set; }

        [JsonProperty("orders.credits")]
        public string Credits { get; set; }

        [JsonProperty("orders.debits")]
        public string Debits { get; set; }

        [JsonProperty("orders.merchant")]
        public string Merchant { get; set; }

        [JsonProperty("orders.refunds")]
        public string Refunds { get; set; }

        [JsonProperty("orders.reversals")]
        public string Reversals { get; set; }

    }
}
