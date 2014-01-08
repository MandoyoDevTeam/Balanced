using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class BankAccountList : BalancedList
    {
        
        [JsonProperty("bank_accounts")]
        public List<BankAccount> BankAccounts { get; set; }
            
        [JsonProperty("links")]
        public BankAccountLink Links { get; set; }

    }
}
