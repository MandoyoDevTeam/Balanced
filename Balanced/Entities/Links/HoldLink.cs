using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class HoldLink 
    {
        [JsonProperty("card")]
        public string Card { get; set; }

        [JsonProperty("debit")]
        public string Debit { get; set; }

    }
}
