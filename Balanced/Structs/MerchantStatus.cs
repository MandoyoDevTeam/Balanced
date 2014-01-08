using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Balanced
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MerchantStatus
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        [EnumMember(Value = "need-more-information")]
        MoreInformation = 1,

        [EnumMember(Value = "underwritten")]
        Underwritten = 2,

        [EnumMember(Value = "rejected")]
        Rejected = 3,
    }
}
