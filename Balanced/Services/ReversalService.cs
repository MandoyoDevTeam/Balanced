using System;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class ReversalService : BalancedServices<Reversal, ReversalList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/reversals");
            }
        }

        public ReversalList Create(Reversal reversal, Credit credit)
        {
            if (reversal == null) throw new ArgumentNullException("reversal", "Reversal can not be null");

            if (credit == null) throw new ArgumentNullException("credit", "Credit can not be null");
            if (string.IsNullOrEmpty(credit.Href)) throw new ArgumentNullException("credit", "Credit Id can not be null");
            
            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Reversal).GetProperty("Amount")), reversal.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Reversal).GetProperty("Description")), reversal.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Reversal).GetProperty("Meta")), reversal.Meta == null ? new JObject() : JToken.FromObject(reversal.Meta) },                
            };

            return BalancedJsonSerializer.DeSerialize<ReversalList>(BalancedHttpRest.Post(string.Format("{0}{1}", credit.Href, RootUri), parameters));
        }

        public new ReversalList Get(Reversal reversal)
        {
            return base.Get(reversal);
        }

        public new ReversalList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new ReversalList Update(Reversal reversal)
        {
            if (reversal == null) throw new ArgumentNullException("reversal", "Reversal can not be null");
            if (string.IsNullOrEmpty(reversal.Href)) throw new ArgumentNullException("reversal", "Reversal Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Reversal).GetProperty("Description")), reversal.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Reversal).GetProperty("Meta")), reversal.Meta == null ? new JObject() : JToken.FromObject(reversal.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<ReversalList>(BalancedHttpRest.Put(string.Format("{0}", reversal.Href), parameters));
        }
    }
}
