using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class BankAccountListLink 
    {
        [JsonProperty("bank_accounts.bank_account_verification")]
        public string BankAccountVerification { get; set; }

        [JsonProperty("bank_accounts.bank_account_verifications")]
        public string Verifications { get; set; }

        [JsonProperty("bank_accounts.credits")]
        public string Credits { get; set; }

        [JsonProperty("bank_accounts.customer")]
        public string Customer { get; set; }

        [JsonProperty("bank_accounts.debits")]
        public string Debits { get; set; }
    }
}
