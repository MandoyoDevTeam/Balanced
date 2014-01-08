using System;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class CardService : BalancedServices<Card, CardList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/cards");
            }
        }

        public new CardList Create(Card card)
        {
            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.CardNumber)) throw new ArgumentNullException("card", "Card Number can not be null");
            if (card.ExpirationYear < DateTime.UtcNow.Year) throw new ArgumentNullException("card", "Card Year is not valid");
            if (card.ExpirationMonth < 1 || card.ExpirationMonth > 12) throw new ArgumentNullException("card", "Card Month is not valid");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("CardNumber")), card.CardNumber },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("ExpirationYear")), card.ExpirationYear },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("ExpirationMonth")), card.ExpirationMonth },                
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("Name")), card.Name },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("SecurityCode")), card.SecurityCode },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("PhoneNumber")), card.PhoneNumber },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Address).GetProperty("City")), card.Address.City },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Address).GetProperty("PostalCode")), card.Address.PostalCode },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Address).GetProperty("StreetAddress")), string.Format("{0},{1}", card.Address.Line1, card.Address.Line2) },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Address).GetProperty("CountryCode")), card.Address.CountryCode },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("Verify")), card.Verify.ToString().ToLower() },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("Meta")), card.Meta == null ? new JObject() : JToken.FromObject(card.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<CardList>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public new CardList Get(Card card)
        {
            return base.Get(card);
        }

        public new CardList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new CardList Update(Card card)
        {
            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Href)) throw new ArgumentNullException("card", "Card Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(CardLink).GetProperty("Customer")), card.Links.Customer },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("Meta")), card.Meta == null ? new JObject() : JToken.FromObject(card.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<CardList>(BalancedHttpRest.Put(string.Format("{0}", card.Href), parameters));
        }

        public DebitList Charge(Card card, Debit debit)
        {
            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Href)) throw new ArgumentNullException("card", "Card Id can not be null");

            if (debit == null) throw new ArgumentNullException("debit", "Debit can not be null");
            if (debit.Amount <= 0) throw new ArgumentNullException("debit", "Debit Amount can not be 0");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Amount")), debit.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("AppearsOnStatementAs")), debit.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Debit).GetProperty("Description")), debit.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(DebitLink).GetProperty("Order")), debit.Links == null ? null : debit.Links.Order },

                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("Meta")), card.Meta == null ? new JObject() : JToken.FromObject(card.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<DebitList>(BalancedHttpRest.Post(string.Format("{0}/debits", card.Href), parameters));
        }

        public new bool Delete(Card card)
        {
            return base.Delete(card);
        }
    }
}