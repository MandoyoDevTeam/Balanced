using System;
using System.Linq;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class CustomerService : BalancedServices<Customer>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/{0}/customers", BalancedHttpRest.Version);
            }
        }

        public CustomerService(string secret) : base(secret)
        {}

        public new Customer Create(Customer customer)
        {
            if(customer == null) throw new ArgumentNullException("customer","Customer can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Name")), customer.Name },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Email")), customer.Email },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("SSNLast4")), customer.SSNLast4 },                
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("BusinessName")), customer.BusinessName },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Phone")), customer.Phone },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("DateOfBirth")), customer.DateOfBirth.HasValue ? string.Format("{0:yyyy-MM}", customer.DateOfBirth) : null },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Ein")), customer.Ein },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Facebook")), customer.Facebook },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Twitter")), customer.Twitter },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Address")), customer.Address == null ? new JObject() : JToken.FromObject(customer.Address) },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Meta")), customer.Meta == null ? new JObject() : JToken.FromObject(customer.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<Customer>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public new Customer Get(Customer customer)
        {            
            return base.Get(customer);
        }

        public new PagedList<Customer> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public Customer AddCard(Customer customer, Card card)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.Id)) throw new ArgumentNullException("customer", "Customer Id can not be null");

            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Uri)) throw new ArgumentNullException("card", "Card Uri can not be null");

            var parameters = new JObject
            {
                { "card_uri", card.Uri }
            };
            return BalancedJsonSerializer.DeSerialize<Customer>(BalancedHttpRest.Put(string.Format("{0}", customer.Uri), parameters));
        }

        public Customer AddBankAccount(Customer customer, BankAccount bankAccount)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.Id)) throw new ArgumentNullException("customer", "Customer Id can not be null");

            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.Uri)) throw new ArgumentNullException("bankAccount", "Bank Account Uri can not be null");

            var parameters = new JObject
            {
                { "bank_account_uri", bankAccount.Uri }
            };
            return BalancedJsonSerializer.DeSerialize<Customer>(BalancedHttpRest.Put(string.Format("{0}", customer.Uri), parameters));
        }

        public new bool Delete(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.Id)) throw new ArgumentNullException("customer", "Customer Id can not be null");

            return base.Delete(customer);
        }
    }
}
