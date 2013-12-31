using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class CallbackStatuses : BalancedObject
    {
        [JsonProperty("failed")]
        public int Failed { get; set; }

        [JsonProperty("pending")]
        public int Pending { get; set; }

        [JsonProperty("retrying")]
        public int Retrying { get; set; }

        [JsonProperty("succeeded")]
        public int Succeeded { get; set; }
    }
}
