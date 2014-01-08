using System;
using Balanced.Entities;
using Balanced.Structs;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class BankAccountService : BalancedServices<BankAccount, BankAccountList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/bank_accounts");
            }
        }


        public new BankAccountList Create(BankAccount bankAccount)
        {
            if (bankAccount == null) throw new ArgumentNullException("bankAccount","Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.Name)) throw new ArgumentNullException("bankAccount", "Bank Account Name can not be null");
            if (string.IsNullOrEmpty(bankAccount.AccountNumber)) throw new ArgumentNullException("bankAccount", "Bank Account Number can not be null");
            if (bankAccount.Type == BankAccountType.Unknown) throw new ArgumentNullException("bankAccount", "Bank Account Type can not be null");
            if (string.IsNullOrEmpty(bankAccount.RoutingNumber)) throw new ArgumentNullException("bankAccount", "Bank Account Routing Number can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("Name")), bankAccount.Name },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("AccountNumber")), bankAccount.AccountNumber },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("RoutingNumber")), bankAccount.RoutingNumber },                
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("Type")), BalancedAttributeHelper.GetEnumAttributes(typeof(BankAccountType).GetMember(bankAccount.Type.ToString())) },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("Meta")), bankAccount.Meta == null ? new JObject() : JToken.FromObject(bankAccount.Meta) },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("Address")), bankAccount.Address == null ? new JObject() : JToken.FromObject(bankAccount.Address) },
            };

            return BalancedJsonSerializer.DeSerialize<BankAccountList>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public new BankAccountList Get(BankAccount bankAccount)
        {
            return base.Get(bankAccount);
        }

        public new BankAccountList List(int limit = 10, int offset = 0)
        {

            return base.List(limit, offset);
        }

        public new BankAccountList Update(BankAccount bankAccount)
        {
            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.Href)) throw new ArgumentNullException("bankAccount", "Bank Account Href can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccountLink).GetProperty("Customer")), bankAccount.Links == null ? null : bankAccount.Links.Customer },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("Meta")), bankAccount.Meta == null ? new JObject() : JToken.FromObject(bankAccount.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<BankAccountList>(BalancedHttpRest.Put(string.Format("{0}", bankAccount.Href), parameters));
        }

        public DebitList Charge(BankAccount bankAccount, Debit debit)
        {
            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (debit == null) throw new ArgumentNullException("debit", "Debit can not be null");

            if (string.IsNullOrEmpty(bankAccount.Href)) throw new ArgumentNullException("bankAccount", "Bank Account Href can not be null");
            if(debit.Amount <= 0) throw new ArgumentNullException("debit", "Debit Amount can not be 0");

            var parameters = new JObject
            {
               { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Amount")), debit.Amount },
               { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("AppearsOnStatementAs")), debit.AppearsOnStatementAs },
               { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Description")), debit.Description },
               { BalancedAttributeHelper.GetPropertyAttributes(typeof(DebitLink).GetProperty("Order")), debit.Links == null ? null : debit.Links.Order },

                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("Meta")), bankAccount.Meta == null ? new JObject() : JToken.FromObject(bankAccount.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<DebitList>(BalancedHttpRest.Post(string.Format("{0}/debits", bankAccount.Href), parameters));
        }

        public new bool Delete(BankAccount bankAccount)
        {
            return base.Delete(bankAccount);
        }
    }
}