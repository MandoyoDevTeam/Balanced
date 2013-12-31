using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class BankAccount : BalancedObject
    {
        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("can_debit")]
        public bool CanDebit { get; set; }

        [JsonProperty("created_at")]
        public string CreatedOn { get; set; }

        [JsonProperty("credits_uri")]
        public string CreditsUri { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonProperty("type")]
        public BankAccountType Type { get; set; }

        [JsonProperty("verification_uri")]
        public string VerificationUri { get; set; }

        [JsonProperty("verifications_uri")]
        public string VerificationsUri { get; set; }  
    }
}
