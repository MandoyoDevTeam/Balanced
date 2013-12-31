using System.Runtime.Serialization;
using Balanced.Structs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Balanced.Entities
{
    [DataContract]
    public class Card : BalancedObject
    {
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("can_debit")]
        public bool CanDebit { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("expiration_month")]
        public int ExpirationMonth { get; set; }

        [JsonProperty("expiration_year")]
        public int ExpirationYear { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("last_four")]
        public int LastFourDigits { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("security_code")]
        public string SecurityCode { get; set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("street_address")]
        public string StreetAddress { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("verify")]
        public bool? Verify { get; set; }

        [JsonProperty("is_verified")]
        public bool? IsVerified { get; set; }

        [JsonProperty("postal_code_check")]
        [JsonConverter(typeof (StringEnumConverter))]
        public PostalCodeStatus PostalCodeCheck { get; set; }

        [JsonProperty("security_code_check")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SecurityCodeStatus SecurityCodeCheck { get; set; }
    }
}
