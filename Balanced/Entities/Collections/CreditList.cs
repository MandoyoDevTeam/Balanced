using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class CreditList : BalancedList
    {

        [JsonProperty("credits")]
        public List<Credit> Credits { get; set; }
            
        [JsonProperty("links")]
        public CreditListLink Links { get; set; }

    }
}
