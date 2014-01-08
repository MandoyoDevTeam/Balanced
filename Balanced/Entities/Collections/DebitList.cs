using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class DebitList : BalancedList
    {

        [JsonProperty("debits")]
        public List<Debit> Debits { get; set; }
            
        [JsonProperty("links")]
        public DebitListLink Links { get; set; }

    }
}
