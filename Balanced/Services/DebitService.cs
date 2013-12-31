using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class DebitService : BalancedServices<Debit>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("{0}/debits", Marketplace.Uri);
            }
        }

        public Marketplace Marketplace { get; private set; }

        public DebitService(string secret, Marketplace marketplace)
            : base(secret)
        {
            if (marketplace == null || string.IsNullOrEmpty(marketplace.Uri)) throw new ArgumentNullException("marketplace");

            Marketplace = marketplace;
        }

        public new Debit Create(Debit debit)
        {
            if (debit == null)throw new ArgumentNullException("debit", "Debit can not be null");
            if (debit.Amount <= 0) throw new ArgumentNullException("debit", "Debit Amount can not be null");

            if (debit.Customer == null) throw new ArgumentNullException("debit", "Debit Customer can not be null");
            if (string.IsNullOrEmpty(debit.Customer.DebitsUri)) throw new ArgumentNullException("debit", "Customer Debits Uri can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Amount")), debit.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("OnBehalfOfUri")), debit.OnBehalfOfUri },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("CustomerUri")), debit.CustomerUri },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Description")), debit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("HoldUri")), debit.HoldUri },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("SourceUri")), debit.SourceUri },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("AppearsOnStatementAs")), debit.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Meta")), debit.Meta == null ? new JObject() : JToken.FromObject(debit.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<Debit>(BalancedHttpRest.Post(string.Format("{0}", debit.Customer.DebitsUri), parameters));
        }

        public new Debit Get(Debit debit)
        {
            return base.Get(debit);
        }

        public new PagedList<Debit> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public PagedList<Debit> ListForCustomer(Customer customer, int limit = 10, int offset = 0)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.DebitsUri)) throw new ArgumentNullException("customer", "Customer Debits Uri can not be null");

            var parameters = new NameValueCollection
            {                
                { "limit", limit.ToString(CultureInfo.InvariantCulture) }, 
                { "offset", offset.ToString(CultureInfo.InvariantCulture) }
            };

            return BalancedJsonSerializer.DeSerialize<PagedList<Debit>>(BalancedHttpRest.Get(customer.DebitsUri, parameters));
        }

        public new Debit Update(Debit debit)
        {
            if (debit == null) throw new ArgumentNullException("debit", "Debit can not be null");
            if (string.IsNullOrEmpty(debit.Uri)) throw new ArgumentNullException("debit", "Debit Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Description")), debit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Meta")), debit.Meta == null ? new JObject() : JToken.FromObject(debit.Meta) },
            };           

            return BalancedJsonSerializer.DeSerialize<Debit>(BalancedHttpRest.Put(string.Format("{0}",debit.Uri), parameters));
        }

        public Refund Refund(Debit debit)
        {
            if (debit == null) throw new ArgumentNullException("debit", "Debit can not be null");
            if (string.IsNullOrEmpty(debit.Id)) throw new ArgumentNullException("debit", "Debits Id can not be null");
            if (string.IsNullOrEmpty(debit.RefundsUri)) throw new ArgumentNullException("debit", "Debits Refunds Uri can not be null");

            return BalancedJsonSerializer.DeSerialize<Refund>(BalancedHttpRest.Post(string.Format("{0}", debit.RefundsUri), null));
        }
    }
}
