
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Balanced.Entities
{
    [DataContract]
    public class Hold : BalancedObject
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("debit")]
        public Debit Debit { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }

        [JsonProperty("fee")]
        public int? Fee { get; set; }

        [JsonProperty("is_void")]
        public bool IsVoid { get; set; }

        [JsonProperty("source")]
        public JObject Source { get; set; }

        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [JsonProperty("events_uri")]
        public string EventsUri { get; set; }

        [JsonProperty("source_uri")]
        public string SourceUri { get; set; }

        [JsonProperty("card_uri")]
        public string CardUri { get; set; }

        [JsonProperty("account_uri")]
        public string AccountUri { get; set; }

        [JsonProperty("transaction_number")]
        public string TransactionNumber { get; set; }

        [JsonProperty("appears_on_statement_as")]
        public string AppearsOnStatementAs { get; set; }    
       
    }
}
