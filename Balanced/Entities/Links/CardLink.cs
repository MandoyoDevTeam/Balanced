using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class CardLink 
    {
        [JsonProperty("customer")]
        public string Customer { get; set; }

    }
}
