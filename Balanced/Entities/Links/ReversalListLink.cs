using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class ReversalListLink
    {
        [JsonProperty("reversals.credit")]
        public string Credit { get; set; }

        [JsonProperty("reversals.events")]
        public string Events { get; set; }

        [JsonProperty("reversals.order")]
        public string Order { get; set; }

    }
}
