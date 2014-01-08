using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Balanced.Structs
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BankAccountType
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        [EnumMember(Value = "checking")]
        Checking = 1,

        [EnumMember(Value = "savings")]
        Savings = 2
    }
}
