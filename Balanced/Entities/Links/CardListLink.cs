using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class CardListLink
    {
        [JsonProperty("cards.card_holds")]
        public string Holds { get; set; }

        [JsonProperty("cards.customer")]
        public string Customer { get; set; }

        [JsonProperty("cards.debits")]
        public string Debits { get; set; }

    }
}
