﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Balanced.Structs
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AvsMatch
    {
        [EnumMember(Value = "unknown")]
        Unknown = 0,

        [EnumMember(Value = "yes")]
        Yes = 1,

        [EnumMember(Value = "no")]
        No = 2,

        [EnumMember(Value = "unsupported")]
        Unsupported = 3,

    }
}