using System;
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

        [JsonProperty("appears_on_statement_as")]
        public string AppearsOnStatementAs { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("debit")]
        public Debit Debit { get; set; }

        [JsonProperty("fee")]
        public int? Fee { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("debit_uri")]
        public string DebitUri { get; set; }

        [JsonProperty("events_uri")]
        public string EventsUri { get; set; }

        [JsonProperty("status")]
        public RefundStatus Status { get; set; }
    }
}
