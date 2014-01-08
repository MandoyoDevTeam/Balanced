using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class DebitLink 
    {
        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

    }
}
