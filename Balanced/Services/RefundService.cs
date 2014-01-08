using System;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class RefundService : BalancedServices<Refund, RefundList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/refunds");
            }
        }

        public RefundList Create(Refund refund, Debit debit)
        {
            if (refund == null) throw new ArgumentNullException("refund", "Refund can not be null");
            if (refund.Amount <= 0) throw new ArgumentNullException("refund", "Refund Amount can not be null");
            
            if (debit == null) throw new ArgumentNullException("debit", "Debit can not be null");
            if (string.IsNullOrEmpty(debit.Href)) throw new ArgumentNullException("debit", "Debit Href can not be null");

           
            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Amount")), refund.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Description")), refund.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Meta")), refund.Meta == null ? new JObject() : JToken.FromObject(refund.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<RefundList>(BalancedHttpRest.Post(string.Format("{0}{1}", debit.Href, RootUri), parameters));
        }

        public new RefundList Get(Refund refund)
        {
            return base.Get(refund);
        }

        public new RefundList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new RefundList Update(Refund refund)
        {
            if (refund == null) throw new ArgumentNullException("refund", "Refund can not be null");
            if (string.IsNullOrEmpty(refund.Href)) throw new ArgumentNullException("refund", "Refund Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Description")), refund.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Meta")), refund.Meta == null ? new JObject() : JToken.FromObject(refund.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<RefundList>(BalancedHttpRest.Put(string.Format("{0}", refund.Href), parameters));
        }
    }
}
