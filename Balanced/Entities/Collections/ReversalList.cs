using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class ReversalList : BalancedList
    {

        [JsonProperty("reversals")]
        public List<Reversal> Reversals { get; set; }
            
        [JsonProperty("links")]
        public ReversalListLink Links { get; set; }

    }
}
