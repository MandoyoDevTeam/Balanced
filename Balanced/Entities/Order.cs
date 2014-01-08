using System;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class Order : BalancedObject
    {

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("amount_escrowed")]
        public int AmountEscrowed { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("delivery_address")]
        public Address Address { get; set; }

        [JsonProperty("links")]
        public OrderLink Links { get; set; }

    }
}
