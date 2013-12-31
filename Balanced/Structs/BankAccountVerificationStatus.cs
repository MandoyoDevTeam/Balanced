using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{
    [DataContract]
    public enum BankAccountVerificationStatus
    {
        [JsonProperty("unknown")]
        Unknown = 0,

        [JsonProperty("created")]
        Created = 1,

        [JsonProperty("deposited")]
        Deposited = 2,

        [JsonProperty("failed")]
        Failed = 3,

        [JsonProperty("succeeded")]
        Succeeded = 4,

        [JsonProperty("updated")]
        Updated = 5,

        [JsonProperty("verified")]
        Verified = 6
    }
}
