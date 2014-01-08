using System;
using Balanced.Config;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class CustomerService : BalancedServices<Customer, CustomerList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/customers");
            }
        }

        public new CustomerList Create(Customer customer)
        {
            if(customer == null) throw new ArgumentNullException("customer","Customer can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Name")), customer.Name },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Email")), customer.Email },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("SSNLast4")), customer.SSNLast4 },                
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("BusinessName")), customer.BusinessName },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Phone")), customer.Phone },
                { "dob", string.Format("{0:yyyy}-{1:MM}", customer.DobYear, customer.DobMonth)},
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Ein")), customer.Ein },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Facebook")), customer.Facebook },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Twitter")), customer.Twitter },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Address")), customer.Address == null ? new JObject() : JToken.FromObject(customer.Address) },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Meta")), customer.Meta == null ? new JObject() : JToken.FromObject(customer.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<CustomerList>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public new CustomerList Get(Customer customer)
        {            
            return base.Get(customer);
        }

        public new CustomerList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new CustomerList Update(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.Href)) throw new ArgumentNullException("customer", "Customer Href can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Name")), customer.Name },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Email")), customer.Email },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("SSNLast4")), customer.SSNLast4 },                
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("BusinessName")), customer.BusinessName },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Phone")), customer.Phone },
                { "dob", string.Format("{0:yyyy}-{1:MM}", customer.DobYear, customer.DobMonth)},
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Ein")), customer.Ein },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Facebook")), customer.Facebook },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Twitter")), customer.Twitter },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Address")), customer.Address == null ? new JObject() : JToken.FromObject(customer.Address) },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Customer).GetProperty("Meta")), customer.Meta == null ? new JObject() : JToken.FromObject(customer.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<CustomerList>(BalancedHttpRest.Put(string.Format("{0}", customer.Href), parameters));
        }

        public CustomerList AssociateCard(Customer customer, Card card)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.Href)) throw new ArgumentNullException("customer", "Customer Href can not be null");

            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Href)) throw new ArgumentNullException("card", "Card Href can not be null");

            var parameters = new JObject
            {
                { "card_uri", card.Href }
            };
            return BalancedJsonSerializer.DeSerialize<CustomerList>(BalancedHttpRest.Put(string.Format("{0}", customer.Href), parameters));
        }

        public CustomerList AssociateBankAccount(Customer customer, BankAccount bankAccount)
        {
            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.Id)) throw new ArgumentNullException("customer", "Customer Id can not be null");

            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.Href)) throw new ArgumentNullException("bankAccount", "Bank Account Uri can not be null");

            var parameters = new JObject
            {
                { "bank_account_uri", bankAccount.Href }
            };
            return BalancedJsonSerializer.DeSerialize<CustomerList>(BalancedHttpRest.Put(string.Format("{0}", customer.Href), parameters));
        }

        public new bool Delete(Customer customer)
        {
            return base.Delete(customer);
        }
    }
}
