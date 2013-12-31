using System;
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

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("remaining_attempts")]
        public int AttemptsLeft { get; set; }

        [JsonProperty("state")]
        public VerificationState State { get; set; }      
    }
}
