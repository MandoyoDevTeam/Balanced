using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class HoldListLink
    {
        [JsonProperty("card_holds.card")]
        public string Card { get; set; }

        [JsonProperty("card_holds.debit")]
        public string Debit { get; set; }

        [JsonProperty("card_holds.debits")]
        public string Debits { get; set; }

        [JsonProperty("card_holds.events")]
        public string Events { get; set; }

    }
}
