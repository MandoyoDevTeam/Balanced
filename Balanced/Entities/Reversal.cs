using Balanced.Structs;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class Reversal : BalancedObject
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        [JsonProperty("failure_reason_code")]
        public string FailureReasonCode { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public Status Status { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("links")]
        public ReversalLink Links { get; set; }

    }
}
