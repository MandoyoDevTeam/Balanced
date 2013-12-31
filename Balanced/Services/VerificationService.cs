using System;
using System.Collections.Specialized;
using System.Globalization;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class VerificationService : BalancedServices<Verification>
    {

        public override string RootUri
        {
            get
            {
                return "/verifications";
            }
        }

        public VerificationService(string secret) : base(secret)
        {
        }

        public Verification Create(BankAccount bankAccount)
        {
            if(bankAccount == null) throw new ArgumentNullException("bankAccount","Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.VerificationsUri)) throw new ArgumentNullException("bankAccount", "Bank Account Verification Uri can not be null");

            return BalancedJsonSerializer.DeSerialize<Verification>(BalancedHttpRest.Post(string.Format("{0}", bankAccount.VerificationsUri), null));
        }

        public Verification Get(BankAccount bankAccount, Verification verification)
        {
            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.VerificationUri)) throw new ArgumentNullException("bankAccount", "Bank Account Verification Uri can not be null");

            return BalancedJsonSerializer.DeSerialize<Verification>(BalancedHttpRest.Get(bankAccount.VerificationUri, null));

        }

        public PagedList<Verification> List(BankAccount bankAccount, int limit = 10, int offset = 0)
        {
            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.VerificationUri)) throw new ArgumentNullException("bankAccount", "Bank Account Verification Uri can not be null");

            var parameters = new NameValueCollection
            {
                { "limit", limit.ToString(CultureInfo.InvariantCulture) }, 
                { "offset", offset.ToString(CultureInfo.InvariantCulture) }
            };

            return BalancedJsonSerializer.DeSerialize<PagedList<Verification>>(BalancedHttpRest.Get(bankAccount.VerificationsUri, parameters));

        }

        public Verification Confirm(Verification verification, int amount1, int amount2)
        {
            if (verification == null) throw new ArgumentNullException("verification", "Verification can not be null");
            if (string.IsNullOrEmpty(verification.Uri)) throw new ArgumentNullException("verification", "Verification Uri can not be null");
            if(amount1 <= 0)throw new ArgumentNullException("amount1", "Amount1 can not be null");
            if (amount2 <= 0) throw new ArgumentNullException("amount2", "Amount1 can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("amount_1")), amount1 },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("amount_2")), amount2 },
            };

            return BalancedJsonSerializer.DeSerialize<Verification>(BalancedHttpRest.Put(string.Format("{0}", verification.Uri), parameters));
        }
    }
}
