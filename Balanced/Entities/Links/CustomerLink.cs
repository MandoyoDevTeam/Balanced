using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class CustomerLink 
    {
        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

    }
}
