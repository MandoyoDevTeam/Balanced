using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class VerificationLink 
    {
        [JsonProperty("bank_account")]
        public string BankAccount { get; set; }

    }
}
