using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class CustomerListLink
    {
        [JsonProperty("customers.bank_accounts")]
        public string BankAccounts { get; set; }

        [JsonProperty("customers.card_holds")]
        public string Holds { get; set; }

        [JsonProperty("customers.cards")]
        public string Cards { get; set; }

        [JsonProperty("customers.credits")]
        public string Credits { get; set; }

        [JsonProperty("customers.debits")]
        public string Debits { get; set; }

        [JsonProperty("customers.destination")]
        public string Destination { get; set; }

        [JsonProperty("customers.orders")]
        public string Orders { get; set; }

        [JsonProperty("customers.refunds")]
        public string Refunds { get; set; }

        [JsonProperty("customers.reversals")]
        public string Reversals { get; set; }

        [JsonProperty("customers.source")]
        public string Source { get; set; }

        [JsonProperty("customers.transactions")]
        public string Transactions { get; set; }

    }
}
