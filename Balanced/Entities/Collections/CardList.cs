using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class CardList : BalancedList
    {

        [JsonProperty("cards")]
        public List<Card> Cards { get; set; }
            
        [JsonProperty("links")]
        public CardListLink Links { get; set; }

    }
}
