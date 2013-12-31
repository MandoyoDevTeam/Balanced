using System;
using System.Linq;
using Balanced.Entities;
using Balanced.Structs;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class BankAccountService : BalancedServices<BankAccount>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/{0}/bank_accounts", BalancedHttpRest.Version);
            }
        }


        public BankAccountService(string secret)
            : base(secret)
        {}        

        public new BankAccount Create(BankAccount bankAccount)
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
            };

            return BalancedJsonSerializer.DeSerialize<BankAccount>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public new BankAccount Get(BankAccount bankAccount)
        {
            return base.Get(bankAccount);
        }

        public new PagedList<BankAccount> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new bool Delete(BankAccount bankAccount)
        {
            return base.Delete(bankAccount);
        }
    }
}
