using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class CustomerList : BalancedList
    {

        [JsonProperty("customers")]
        public List<Customer> Customers { get; set; }
            
        [JsonProperty("links")]
        public CustomerListLink Links { get; set; }

    }
}
