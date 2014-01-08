using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class Customer : BalancedObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("business_name")]
        public string BusinessName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("ssn_last4")]
        public string SSNLast4 { get; set; }

        [JsonProperty("ein")]
        public string Ein { get; set; }

        [JsonProperty("dob_month")]
        public string DobMonth { get; set; }

        [JsonProperty("dob_year")]
        public string DobYear { get; set; }

        [JsonProperty("merchant_status", NullValueHandling = NullValueHandling.Ignore)]
        public MerchantStatus MerchantStatus { get; set; }
        
        [JsonProperty("address")]
        public Address Address { get; set; }
        
        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("is_identity_verified")]
        public bool IsIdentityVerified { get; set; }

        [JsonProperty("links")]
        public CustomerLink Links { get; set; }

    }
}
