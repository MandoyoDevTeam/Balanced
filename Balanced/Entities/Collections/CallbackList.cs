using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class CallbackList : BalancedList
    {

        [JsonProperty("callbacks")]
        public List<Callback> Callbacks { get; set; }
            
        [JsonProperty("links")]
        public Dictionary<string,string> Links { get; set; }

    }
}
