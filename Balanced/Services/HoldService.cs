using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class HoldService : BalancedServices<Hold>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("{0}/holds", Marketplace.Uri);
            }
        }

        public Marketplace Marketplace { get; private set; }

        public HoldService(string secret, Marketplace marketplace)
            : base(secret)
        {
            if (marketplace == null || string.IsNullOrEmpty(marketplace.Uri)) throw new ArgumentNullException("marketplace");

            Marketplace = marketplace;
        }

        public new Hold Create(Hold hold)
        {
            if (hold == null) throw new ArgumentNullException("hold", "Hold can not be null");
            if (hold.Customer == null) throw new ArgumentNullException("hold", "Hold Customer can not be null");
            if (hold.Amount <= 0) throw new ArgumentNullException("hold", "Hold Amount can not be null");
            if (string.IsNullOrEmpty(hold.SourceUri) && string.IsNullOrEmpty(hold.CardUri)) throw new ArgumentNullException("hold", "Hold Source/Card Uri can not be null");
            if (!string.IsNullOrEmpty(hold.SourceUri) && !string.IsNullOrEmpty(hold.CardUri)) throw new ArgumentNullException("hold", "You can only define a Source Uri or Card Uri");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Amount")), hold.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("AccountUri")), hold.AccountUri },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("CardUri")), hold.CardUri },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Description")), hold.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("SourceUri")), hold.SourceUri },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("AppearsOnStatementAs")), hold.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Meta")), hold.Meta == null ? new JObject() : JToken.FromObject(hold.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<Hold>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public new Hold Get(Hold hold)
        {
            return base.Get(hold);
        }

        public new PagedList<Hold> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public PagedList<Hold> ListForCustomer(Customer customer, int limit = 10, int offset = 0)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.HoldsUri)) throw new ArgumentNullException("customer", "Customer Holds Uri can not be null");

            var parameters = new NameValueCollection
            {                
                { "limit", limit.ToString(CultureInfo.InvariantCulture) }, 
                { "offset", offset.ToString(CultureInfo.InvariantCulture) }
            };

            return BalancedJsonSerializer.DeSerialize<PagedList<Hold>>(BalancedHttpRest.Get(customer.HoldsUri, parameters));
        }

        public new Hold Update(Hold hold)
        {
            if (hold == null) throw new ArgumentNullException("hold", "Hold can not be null");
            if (string.IsNullOrEmpty(hold.Uri)) throw new ArgumentNullException("hold", "Hold Uri can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Description")), hold.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Meta")), hold.Meta == null ? new JObject() : JToken.FromObject(hold.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<Hold>(BalancedHttpRest.Put(string.Format("{0}", hold.Uri), parameters));
        }

        public Debit Capture(Hold hold)
        {
            if (hold == null) throw new ArgumentNullException("hold", "Hold can not be null");
            if (hold.Customer == null) throw new ArgumentNullException("hold", "Hold Customer can not be null");
            if (string.IsNullOrEmpty(hold.Customer.HoldsUri)) throw new ArgumentNullException("hold", "Hold Customer Holds Uri can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Amount")), hold.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Description")), hold.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("AppearsOnStatementAs")), hold.AppearsOnStatementAs },
            };

            return BalancedJsonSerializer.DeSerialize<Debit>(BalancedHttpRest.Post(string.Format("{0}", hold.Customer.HoldsUri), parameters));
        }

        public Hold Void(Hold hold)
        {
            if (hold == null) throw new ArgumentNullException("hold", "Hold can not be null");
            if (string.IsNullOrEmpty(hold.Uri)) throw new ArgumentNullException("hold", "Hold Uri can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("IsVoid")), true },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("AppearsOnStatementAs")), hold.AppearsOnStatementAs },
            };

            return BalancedJsonSerializer.DeSerialize<Hold>(BalancedHttpRest.Put(string.Format("{0}", hold.Uri), parameters));
        }
    }
}
