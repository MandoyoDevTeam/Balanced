using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Verification : BalancedObject
    {
        [JsonProperty("attempts")]
        public int Attempts { get; set; }

        [JsonProperty("attempts_remaining")]
        public int AttemptsLeft { get; set; }

        [JsonProperty("verification_status", NullValueHandling = NullValueHandling.Ignore)]
        public Status VerificationStatus { get; set; }

        [JsonProperty("deposit_status", NullValueHandling = NullValueHandling.Ignore)]
        public Status DepositStatus { get; set; }

        [JsonProperty("links")]
        public VerificationLink Links { get; set; }
    }
}
