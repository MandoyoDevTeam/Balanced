using System;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class DebitService : BalancedServices<Debit, DebitList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/debits");
            }
        }

        public DebitList Create(Debit debit, BankAccount bankAccount)
        {
            if (debit == null)throw new ArgumentNullException("debit", "Debit can not be null");
            if (debit.Amount <= 0) throw new ArgumentNullException("debit", "Debit Amount can not be null");

            if (bankAccount == null) throw new ArgumentNullException("bankAccount", "Bank Account can not be null");
            if (string.IsNullOrEmpty(bankAccount.Href)) throw new ArgumentNullException("bankAccount", "Bank Account Href can not be null");


            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Amount")), debit.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Description")), debit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("AppearsOnStatementAs")), debit.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(DebitLink).GetProperty("Order")), debit.Links == null ? null : debit.Links.Order },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Meta")), debit.Meta == null ? new JObject() : JToken.FromObject(debit.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<DebitList>(BalancedHttpRest.Post(string.Format("{0}{1}", bankAccount.Href, RootUri), parameters));
        }

        public DebitList Create(Debit debit, Card card)
        {
            if (debit == null) throw new ArgumentNullException("debit", "Debit can not be null");
            if (debit.Amount <= 0) throw new ArgumentNullException("debit", "Debit Amount can not be null");

            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Href)) throw new ArgumentNullException("card", "Card Href can not be null");


            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Amount")), debit.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Description")), debit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("AppearsOnStatementAs")), debit.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(DebitLink).GetProperty("Order")), debit.Links == null ? null : debit.Links.Order },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Meta")), debit.Meta == null ? new JObject() : JToken.FromObject(debit.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<DebitList>(BalancedHttpRest.Post(string.Format("{0}{1}", card.Href, RootUri), parameters));
        }

        public new DebitList Get(Debit debit)
        {
            return base.Get(debit);
        }

        public new DebitList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new DebitList Update(Debit debit)
        {
            if (debit == null) throw new ArgumentNullException("debit", "Debit can not be null");
            if (string.IsNullOrEmpty(debit.Href)) throw new ArgumentNullException("debit", "Debit Href can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Description")), debit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Meta")), debit.Meta == null ? new JObject() : JToken.FromObject(debit.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<DebitList>(BalancedHttpRest.Put(string.Format("{0}", debit.Href), parameters));
        }

    }
}
