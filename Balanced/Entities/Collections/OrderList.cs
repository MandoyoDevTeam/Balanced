using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class OrderList : BalancedList
    {

        [JsonProperty("orders")]
        public List<Order> Orders { get; set; }
            
        [JsonProperty("links")]
        public OrderListLink Links { get; set; }

    }
}
