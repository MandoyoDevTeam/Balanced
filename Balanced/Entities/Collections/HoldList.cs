using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class HoldList : BalancedList
    {

        [JsonProperty("card_holds")]
        public List<Hold> Holds { get; set; }
            
        [JsonProperty("links")]
        public HoldListLink Links { get; set; }

    }
}
