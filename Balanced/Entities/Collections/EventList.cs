using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Balanced.Entities
{
    [DataContract]
    public class EventList : BalancedList
    {

        [JsonProperty("events")]
        public List<Event> Events { get; set; }
            
        [JsonProperty("links")]
        public EventListLink Links { get; set; }

    }
}
