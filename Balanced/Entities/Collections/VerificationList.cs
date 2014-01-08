using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class VerificationList : BalancedList
    {

        [JsonProperty("bank_account_verifications")]
        public List<Verification> Verifications { get; set; }
            
        [JsonProperty("links")]
        public VerificationListLink Links { get; set; }

    }
}
