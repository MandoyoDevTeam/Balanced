using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Refund : BalancedObject
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public Status Status { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("links")]
        public RefundLink Links { get; set; }
    }
}
