using System;
using System.Collections.Specialized;
using System.Globalization;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class RefundService : BalancedServices<Refund>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("{0}/refunds", Marketplace.Uri);
            }
        }

        public Marketplace Marketplace { get; private set; }

        public RefundService(string secret, Marketplace marketplace)
            : base(secret)
        {
            if (marketplace == null || string.IsNullOrEmpty(marketplace.Uri)) throw new ArgumentNullException("marketplace");

            Marketplace = marketplace;
        }

        public new Refund Create(Refund refund)
        {
            if (refund == null) throw new ArgumentNullException("refund", "Refund can not be null");
            if (refund.Amount <= 0) throw new ArgumentNullException("refund", "Refund Amount can not be null");
            if (string.IsNullOrEmpty(refund.DebitUri) && (refund.Debit == null || string.IsNullOrEmpty(refund.Debit.Uri))) throw new ArgumentNullException("refund", "Debit Uri can not be null");

            if (refund.Customer == null) throw new ArgumentNullException("refund", "Refund Customer can not be null");
            if (string.IsNullOrEmpty(refund.Customer.RefundsUri)) throw new ArgumentNullException("refund", "Customer Refunds Uri can not be null");
           
            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Amount")), refund.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("DebitUri")), string.IsNullOrEmpty(refund.DebitUri) ? refund.Debit.Uri : refund.DebitUri },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Description")), refund.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Meta")), refund.Meta == null ? new JObject() : JToken.FromObject(refund.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<Refund>(BalancedHttpRest.Post(string.Format("{0}", refund.Customer.RefundsUri), parameters));
        }

        public new Refund Get(Refund refund)
        {
            return base.Get(refund);
        }

        public new PagedList<Refund> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public PagedList<Refund> ListForCustomer(Customer customer, int limit = 10, int offset = 0)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.RefundsUri)) throw new ArgumentNullException("customer", "Customer Refunds Uri can not be null");

            var parameters = new NameValueCollection
            {                
                { "limit", limit.ToString(CultureInfo.InvariantCulture) }, 
                { "offset", offset.ToString(CultureInfo.InvariantCulture) }
            };

            return BalancedJsonSerializer.DeSerialize<PagedList<Refund>>(BalancedHttpRest.Get(customer.RefundsUri, parameters));
        }

        public new Refund Update(Refund refund)
        {
            if (refund == null) throw new ArgumentNullException("refund", "Refund can not be null");
            if (string.IsNullOrEmpty(refund.Uri)) throw new ArgumentNullException("refund", "Refund Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Description")), refund.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Refund).GetProperty("Meta")), refund.Meta == null ? new JObject() : JToken.FromObject(refund.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<Refund>(BalancedHttpRest.Put(string.Format("{0}", refund.Uri), parameters));
        }
    }
}
