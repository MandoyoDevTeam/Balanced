using System;
using Balanced.Entities;
using Newtonsoft.Json;

namespace Balanced.Helpers
{
    public class EntitySerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(BankAccountList).IsAssignableFrom(objectType) || 
                typeof(VerificationList).IsAssignableFrom(objectType) || 
                typeof(HoldList).IsAssignableFrom(objectType) || 
                typeof(CreditList).IsAssignableFrom(objectType) ||
                typeof(CustomerList).IsAssignableFrom(objectType) || 
                typeof(DebitList).IsAssignableFrom(objectType) ||
                typeof(RefundList).IsAssignableFrom(objectType) || 
                typeof(ReversalList).IsAssignableFrom(objectType);
        }
    }
}
