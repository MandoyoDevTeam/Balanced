using Newtonsoft.Json;

namespace Balanced.Helpers
{
    public class BalancedJsonSerializer
    {
        public static string Serialize<T>(T item)
        {
            //json .net
            return JsonConvert.SerializeObject(item, Formatting.Indented);
        }

        public static T DeSerialize<T>(string text)
        {
            //json .net
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
