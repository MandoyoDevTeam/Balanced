using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Marketplace : BalancedObject
    {
        [JsonProperty("in_escrow")]
        public float InEscrow { get; set; }

        [JsonProperty("callbacks_uri")]
        public string CallBacksUri { get; set; }

        [JsonProperty("domain_url")]
        public string DomainUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("transactions_uri")]
        public string TransactionsUri { get; set; }

        [JsonProperty("support_email_address")]
        public string SupportEmailAddress { get; set; }

        [JsonProperty("events_uri")]
        public string EventsUri { get; set; }

        [JsonProperty("customers_uri")]
        public string CustomersUri { get; set; }

        [JsonProperty("support_phone_number")]
        public string SupportPhoneNumber { get; set; }

        [JsonProperty("refunds_uri")]
        public string RefundsUri { get; set; }

        [JsonProperty("debits_uri")]
        public string DebitsUri { get; set; }

        [JsonProperty("holds_uri")]
        public string HoldsUri { get; set; }

        [JsonProperty("bank_accounts_uri")]
        public string BankAccountsUri { get; set; }

        [JsonProperty("credits_uri")]
        public string CreditsUri { get; set; }

        [JsonProperty("cards_uri")]
        public string CardsUri { get; set; }
    }
}
