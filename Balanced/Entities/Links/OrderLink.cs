using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class OrderLink 
    {
        [JsonProperty("merchant")]
        public string Merchant { get; set; }

    }
}
