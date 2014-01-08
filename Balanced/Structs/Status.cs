using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Balanced.Structs
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        [EnumMember(Value = "pending")]
        Pending = 1,

        [EnumMember(Value = "failed")]
        Failed = 2,

        [EnumMember(Value = "succeeded")]
        Succeeded = 3,
     
    }
}
