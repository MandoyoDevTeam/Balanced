using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Balanced.Entities
{
    [DataContract]
    public class Card : BalancedObject
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }


        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("expiration_month")]
        public int ExpirationMonth { get; set; }

        [JsonProperty("expiration_year")]
        public int ExpirationYear { get; set; }

        [JsonProperty("security_code")]
        public string SecurityCode { get; set; }

        [JsonProperty("cvv")]
        public string Cvv { get; set; }

        [JsonProperty("cvv_match", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public CvvMatch CvvMatch { get; set; }

        [JsonProperty("cvv_result")]
        public string CvvResult { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("avs_street_match", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public AvsMatch StreetMatch { get; set; }

        [JsonProperty("avs_postal_match", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public AvsMatch PostalMatch { get; set; }

        [JsonProperty("avs_result")]
        public string AvsResult { get; set; }

        [JsonProperty("fingerprint")]
        public string Fingerprint { get; set; }
        
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("is_verified")]
        public bool? IsVerified { get; set; }

        [JsonProperty("verify")]
        public bool? Verify { get; set; }

        [JsonProperty("links")]
        public CardLink Links { get; set; }
    }
}
