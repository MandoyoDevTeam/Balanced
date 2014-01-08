using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Hold : BalancedObject
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_void")]
        public bool IsVoid { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        [JsonProperty("failure_reason_code")]
        public string FailureReasonCode { get; set; }

        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("appears_on_statement_as")]
        public string AppearsOnStatementAs { get; set; }

        [JsonProperty("links")]
        public HoldLink Links { get; set; }
       
    }
}
