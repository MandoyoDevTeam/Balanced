using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Credit : BalancedObject
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("bank_account")]
        public BankAccount BankAccount { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("appears_on_statement_as")]
        public string AppearsOnStatementAs { get; set; }

        [JsonProperty("events_uri")]
        public string EventsUri { get; set; }

        [JsonProperty("reversals_uri")]
        public string ReversalsUri { get; set; }

        [JsonProperty("available_at")]
        public DateTime? AvailableAt { get; set; }

        [JsonProperty("fee")]
        public int? Fee { get; set; }

        [JsonProperty("destination")]
        public dynamic Destination { get; set; }

        [JsonProperty("state")]
        public CreditState State { get; set; }

        [JsonProperty("status")]
        public CreditStatus Status { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }
    }
}
