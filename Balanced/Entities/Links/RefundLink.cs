using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class RefundLink 
    {
        [JsonProperty("debit")]
        public string Debit { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

    }
}
