using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Structs
{
    [DataContract]
    public enum BankAccountType
    {
        [JsonProperty("unknown")]
        Unknown = 0,

        [JsonProperty("checking")]
        Checking = 1,

        [JsonProperty("savings")]
        Savings = 2
    }
}
