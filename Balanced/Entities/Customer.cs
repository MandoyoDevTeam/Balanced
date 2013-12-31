
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Customer : BalancedObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("ssn_last4")]
        public string SSNLast4 { get; set; }

        [JsonProperty("business_name")]
        public string BusinessName { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("dob")]
        public DateTime? DateOfBirth { get; set; }

        [JsonProperty("ein")]
        public string Ein { get; set; }

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("bank_accounts_uri")]
        public string BankAccountsUri { get; set; }

        [JsonProperty("cards_uri")]
        public string CardsUri { get; set; }

        [JsonProperty("credits_uri")]
        public string CreditsUri { get; set; }

        [JsonProperty("debits_uri")]
        public string DebitsUri { get; set; }

        [JsonProperty("holds_uri")]
        public string HoldsUri { get; set; }

        [JsonProperty("refunds_uri")]
        public string RefundsUri { get; set; }

        [JsonProperty("reversals_uri")]
        public string ReversalsUri { get; set; }

        [JsonProperty("transactions_uri")]
        public string TransactionsUri { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("is_identity_verified")]
        public bool IsIdentityVerified { get; set; }
        
    }
}
