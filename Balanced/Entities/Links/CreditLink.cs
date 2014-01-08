using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class CreditLink 
    {
        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

    }
}
