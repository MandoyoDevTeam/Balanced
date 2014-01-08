using Newtonsoft.Json;

namespace Balanced.Entities
{
    public class BalancedList
    {

        [JsonProperty("meta")]
        public PagedList Pagination { get; set; }
    }
}
