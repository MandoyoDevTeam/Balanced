using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class ReversalLink 
    {
        [JsonProperty("credit")]
        public string Credit { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

    }
}
