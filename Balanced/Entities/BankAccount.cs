using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class BankAccount : BalancedObject
    {
        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("can_debit")]
        public bool CanDebit { get; set; }

        [JsonProperty("can_credit")]
        public bool CanCredit { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonProperty("account_type", NullValueHandling = NullValueHandling.Ignore)]
        public BankAccountType Type { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("links")]
        public BankAccountLink Links { get; set; }

    }
}
