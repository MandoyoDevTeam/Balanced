using System;
using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Balanced.Entities
{
    [DataContract]
    public class Debit : BalancedObject
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("appears_on_statement_as")]
        public string AppearsOnStatementAs { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public Status Status { get; set; }

        [JsonProperty("failure_reason")]
        public string FailureReason { get; set; }

        [JsonProperty("failure_reason_code")]
        public string FailureReasonCode { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("links")]
        public DebitLink Links { get; set; }
        
    }
}
