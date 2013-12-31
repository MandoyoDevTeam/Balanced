using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    [KnownType(typeof(BankAccount))]
    [KnownType(typeof(Callback))]
    [KnownType(typeof(Card))]
    [KnownType(typeof(Credit))]
    [KnownType(typeof(Customer))]
    [KnownType(typeof(Debit))]
    [KnownType(typeof(Event))]
    [KnownType(typeof(Hold))]
    [KnownType(typeof(Marketplace))]
    [KnownType(typeof(Refund))]
    [KnownType(typeof(Verification))]
    public class PagedList<T> where T : BalancedObject
    {
        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("first_uri")]
        public string FirstUri { get; set; }

        [JsonProperty("last_uri")]
        public string LastUri { get; set; }

        [JsonProperty("next_uri")]
        public string NextUri { get; set; }

        [JsonProperty("previous_uri")]
        public string PreviousUri { get; set; }
    }
}
