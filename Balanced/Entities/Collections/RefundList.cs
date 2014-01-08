using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class RefundList : BalancedList
    {

        [JsonProperty("refunds")]
        public List<Refund> Refunds { get; set; }
            
        [JsonProperty("links")]
        public RefundListLink Links { get; set; }

    }
}
