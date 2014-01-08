using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class VerificationListLink
    {
        [JsonProperty("bank_account_verifications.bank_account")]
        public string BankAccount { get; set; }

    }
}
