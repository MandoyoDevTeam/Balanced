using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Balanced.Structs
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CallbackMethod
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        [EnumMember(Value = "post")]
        Post = 1,

        [EnumMember(Value = "put")]
        Put = 2,

        [EnumMember(Value = "get")]
        Get = 3,

    }
}
