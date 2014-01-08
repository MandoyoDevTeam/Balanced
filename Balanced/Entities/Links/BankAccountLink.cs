using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class BankAccountLink 
    {
        [JsonProperty("bank_account_verification")]
        public string BankAccountVerification { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

    }
}
