using System;
using System.Linq;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class CardService : BalancedServices<Card>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("{0}/cards", Marketplace.Uri);
            }
        }

        public Marketplace Marketplace { get; private set; }

        public CardService(string secret, Marketplace marketplace) : base(secret)
        {
            if (marketplace == null || string.IsNullOrEmpty(marketplace.Uri)) throw new ArgumentNullException("marketplace");
            
            Marketplace = marketplace;
        }

        public new Card Create(Card card)
        {
            if(card == null) throw new ArgumentNullException("card", "Card can not be null");
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
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("City")), card.City },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("PostalCode")), card.PostalCode },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("StreetAddress")), card.StreetAddress },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("CountryCode")), card.CountryCode },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("Verify")), card.IsValid.ToString().ToLower() },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("Meta")), card.Meta == null ? new JObject() : JToken.FromObject(card.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<Card>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public new Card Get(Card card)
        {
            return base.Get(card);
        }

        public new PagedList<Card> List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new Card Update(Card card)
        {
            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Id)) throw new ArgumentNullException("card", "Card Id can not be null");

            return base.Update(card);
        }

        public Card Invalidate(Card card)
        {
            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Id)) throw new ArgumentNullException("card", "Card Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Card).GetProperty("IsValid")), "false" },
            };

            return BalancedJsonSerializer.DeSerialize<Card>(BalancedHttpRest.Put(string.Format("{0}/{1}", RootUri, card.Id), parameters));
        }

        public new bool Delete(Card card)
        {
            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Id)) throw new ArgumentNullException("card", "Card Id can not be null");

            return base.Delete(card);
        }
    }
}