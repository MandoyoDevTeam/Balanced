using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Balanced.Entities;
using Balanced.Helpers;
using Balanced.Structs;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class CreditService : BalancedServices<Credit>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("{0}/credits", Marketplace.Uri);
            }
        }

        public Marketplace Marketplace { get; private set; }

        public CreditService(string secret, Marketplace marketplace)
            : base(secret)
        {
            if (marketplace == null || string.IsNullOrEmpty(marketplace.Uri)) throw new ArgumentNullException("marketplace");

            Marketplace = marketplace;

        }

        public Credit CreateForNewBankAccount(Credit credit)
        {
            if (credit == null) throw new ArgumentNullException("credit", "Credit can not be null");
            if (credit.Amount <= 0) throw new ArgumentNullException("credit", "Credit Amount can not be null");

            if (credit.BankAccount == null) throw new ArgumentNullException("credit", "Credit Bank Account can not be null");
            if (string.IsNullOrEmpty(credit.BankAccount.Name)) throw new ArgumentNullException("credit", "Bank Account Name can not be null");
            if (string.IsNullOrEmpty(credit.BankAccount.AccountNumber)) throw new ArgumentNullException("credit", "Bank Account Account Number can not be null");
            if (credit.BankAccount.Type == BankAccountType.Unknown) throw new ArgumentNullException("credit", "Bank Account Code can not be null");
            if (string.IsNullOrEmpty(credit.BankAccount.RoutingNumber)) throw new ArgumentNullException("credit", "Bank Account Routing Number can not be null");

            var bankParameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("Name")), credit.BankAccount.Name },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("AccountNumber")), credit.BankAccount.AccountNumber },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("RoutingNumber")), credit.BankAccount.RoutingNumber },                
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("Type")), BalancedAttributeHelper.GetEnumAttributes(typeof(BankAccountType).GetMember(credit.BankAccount.Type.ToString())) }
            };

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Amount")), credit.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Description")), credit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("AppearsOnStatementAs")), credit.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("BankAccount")), bankParameters },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Meta")), credit.Meta == null ? new JObject() : JToken.FromObject(credit.Meta) },                
            };

            return BalancedJsonSerializer.DeSerialize<Credit>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public Credit CreateForExistingBankAccount(Credit credit)
        {
            if (credit == null) throw new ArgumentNullException("credit", "Credit can not be null");
            if (credit.Amount <= 0) throw new ArgumentNullException("credit", "Credit Amount can not be null");

            if (credit.BankAccount == null) throw new ArgumentNullException("credit", "Credit Bank Account can not be null");
            if (string.IsNullOrEmpty(credit.BankAccount.CreditsUri)) throw new ArgumentNullException("credit", "Bank Account Credits Uri can not be null");

             var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Amount")), credit.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Description")), credit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("AppearsOnStatementAs")), credit.AppearsOnStatementAs },
                
            };

            if (credit.Meta != null && credit.Meta.Any())
                foreach (var key in credit.Meta.Keys)
                    parameters.Add(string.Format("meta[{0}]", key), credit.Meta[key]);

            return BalancedJsonSerializer.DeSerialize<Credit>(BalancedHttpRest.Post(string.Format("{0}", credit.BankAccount.CreditsUri), parameters));
        }

        public Credit CreateForExistingCustomer(Credit credit)
        {
            if (credit == null) throw new ArgumentNullException("credit", "Credit can not be null");
            if (credit.Amount <= 0) throw new ArgumentNullException("credit", "Credit Amount can not be null");

            if (credit.Customer == null) throw new ArgumentNullException("credit", "Credit Customer can not be null");
            if (string.IsNullOrEmpty(credit.Customer.CreditsUri)) throw new ArgumentNullException("credit", "Customer Credits Uri can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Amount")), credit.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Description")), credit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("AppearsOnStatementAs")), credit.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Credit).GetProperty("Meta")), credit.Meta == null ? new JObject() : JToken.FromObject(credit.Meta) },                
            };

            return BalancedJsonSerializer.DeSerialize<Credit>(BalancedHttpRest.Post(string.Format("{0}", credit.Customer.CreditsUri), parameters));
        }

        public new Credit Get(Credit credit)
        {
            return base.Get(credit);
        }

        public new PagedList<Credit> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public PagedList<Credit> ListForBankAccount(BankAccount bankAccount, int limit = 10, int offset = 0)
        {
            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.CreditsUri)) throw new ArgumentNullException("bankAccount", "Bank Account Credits Uri can not be null");

            var parameters = new NameValueCollection
            {
                { "limit", limit.ToString(CultureInfo.InvariantCulture) }, 
                { "offset", offset.ToString(CultureInfo.InvariantCulture) }
            };

            return BalancedJsonSerializer.DeSerialize<PagedList<Credit>>(BalancedHttpRest.Get(bankAccount.CreditsUri, parameters));

        }

        public PagedList<Credit> ListForCustomer(Customer customer, int limit = 10, int offset = 0)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.CreditsUri)) throw new ArgumentNullException("customer", "Customer Credits Uri can not be null");

            var parameters = new NameValueCollection
            {                
                { "limit", limit.ToString(CultureInfo.InvariantCulture) }, 
                { "offset", offset.ToString(CultureInfo.InvariantCulture) }
            };

            return BalancedJsonSerializer.DeSerialize<PagedList<Credit>>(BalancedHttpRest.Get(customer.CreditsUri, parameters));
        }
    }
}
