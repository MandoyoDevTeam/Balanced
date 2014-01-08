using System;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class VerificationService : BalancedServices<Verification, VerificationList>
    {

        public override string RootUri
        {
            get
            {
                return "/verifications";
            }
        }

        public VerificationList Create(BankAccount bankAccount)
        {
            if(bankAccount == null) throw new ArgumentNullException("bankAccount","Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.Href)) throw new ArgumentNullException("bankAccount", "Bank Account Verification Uri can not be null");

            return BalancedJsonSerializer.DeSerialize<VerificationList>(BalancedHttpRest.Post(string.Format("{0}{1}", bankAccount.Href, RootUri), null));
        }

        public new VerificationList Get(Verification verification)
        {
            return base.Get(verification);

        }

        public new VerificationList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public VerificationList Confirm(Verification verification, int amount1, int amount2)
        {
            if (verification == null) throw new ArgumentNullException("verification", "Verification can not be null");
            if (string.IsNullOrEmpty(verification.Href)) throw new ArgumentNullException("verification", "Verification Uri can not be null");
            if(amount1 <= 0)throw new ArgumentNullException("amount1", "Amount1 can not be null");
            if (amount2 <= 0) throw new ArgumentNullException("amount2", "Amount1 can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("amount_1")), amount1 },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(BankAccount).GetProperty("amount_2")), amount2 },
            };

            return BalancedJsonSerializer.DeSerialize<VerificationList>(BalancedHttpRest.Put(string.Format("{0}", verification.Href), parameters));
        }
    }
}
