using System;
using System.Collections.Specialized;
using System.Globalization;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class CreditService : BalancedServices<Credit, CreditList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/credits");
            }
        }

        public CreditList Create(Credit credit, BankAccount bankAccount)
        {
            if (credit == null) throw new ArgumentNullException("credit", "Credit can not be null");
            if (credit.Amount <= 0) throw new ArgumentNullException("credit", "Credit Amount can not be null");

            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.Href)) throw new ArgumentNullException("bankAccount", "Bank Account Href can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Amount")), credit.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Description")), credit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("AppearsOnStatementAs")), credit.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(CreditLink).GetProperty("Order")), credit.Links == null ? null : credit.Links.Order },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Meta")), credit.Meta == null ? new JObject() : JToken.FromObject(credit.Meta) },                
            };

            return BalancedJsonSerializer.DeSerialize<CreditList>(BalancedHttpRest.Post(string.Format("{0}{1}", bankAccount.Href, RootUri), parameters));
        }

        public new CreditList Get(Credit credit)
        {
            return base.Get(credit);
        }

        public CreditList ListForBankAccount(BankAccount bankAccount, int limit = 10, int offset = 0)
        {
            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.Href)) throw new ArgumentNullException("bankAccount", "Bank Account Href can not be null");

            var parameters = new NameValueCollection
            {
                { "limit", limit.ToString(CultureInfo.InvariantCulture) }, 
                { "offset", offset.ToString(CultureInfo.InvariantCulture) }
            };

            return BalancedJsonSerializer.DeSerialize<CreditList>(BalancedHttpRest.Get(string.Format("{0}{1}", bankAccount.Href, RootUri), parameters));
            
        }

        public new CreditList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new CreditList Update(Credit credit)
        {
            if (credit == null) throw new ArgumentNullException("credit", "Credit can not be null");
            if (string.IsNullOrEmpty(credit.Href)) throw new ArgumentNullException("credit", "Credit Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Description")), credit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Meta")), credit.Meta == null ? new JObject() : JToken.FromObject(credit.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<CreditList>(BalancedHttpRest.Put(string.Format("{0}", credit.Href), parameters));
        }

    }
}
