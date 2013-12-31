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

        [JsonProperty("appears_on_statement_as")]
        public string AppearsOnStatementAs { get; set; }

        [JsonProperty("available_at")]
        public DateTime AvailableAt { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fee")]
        public int? Fee { get; set; }

        [JsonProperty("on_behalf_of")]
        public string OnBehalfOf { get; set; }

        [JsonProperty("on_behalf_of_uri")]
        public string OnBehalfOfUri { get; set; }

        [JsonProperty("refunds_uri")]
        public string RefundsUri { get; set; }

        [JsonProperty("events_uri")]
        public string EventsUri { get; set; }

        [JsonProperty("source_uri")]
        public string SourceUri { get; set; }

        [JsonProperty("hold_uri")]
        public string HoldUri { get; set; }

        [JsonProperty("customer_uri")]
        public string CustomerUri { get; set; }

        /// <summary>
        /// Could be a <see cref="BankAccount"/> or a <see cref="Card"/>
        /// </summary>
        [JsonProperty("source")]
        public JObject Source { get; set; }

        [JsonProperty("hold")]
        public Hold Hold { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("status")]
        public DebitStatus Status { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }      
        
    }
}
