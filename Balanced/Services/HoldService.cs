using System;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class HoldService : BalancedServices<Hold, HoldList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/holds");
            }
        }

        public new HoldList Create(Hold hold, Card card)
        {
            if (hold == null) throw new ArgumentNullException("hold", "Hold can not be null");
            if (hold.Amount <= 0) throw new ArgumentNullException("hold", "Hold Amount can not be null");

            if (card == null) throw new ArgumentNullException("card", "Card can not be null");
            if (string.IsNullOrEmpty(card.Href)) throw new ArgumentNullException("card", "Card Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Amount")), hold.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Description")), hold.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("AppearsOnStatementAs")), hold.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Meta")), hold.Meta == null ? new JObject() : JToken.FromObject(hold.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<HoldList>(BalancedHttpRest.Post(string.Format("{0}{1}", card.Href, RootUri), parameters));
        }

        public new HoldList Get(Hold hold)
        {
            return base.Get(hold);
        }

        public new HoldList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new HoldList Update(Hold hold)
        {
            if (hold == null) throw new ArgumentNullException("hold", "Hold can not be null");
            if (string.IsNullOrEmpty(hold.Href)) throw new ArgumentNullException("hold", "Hold Uri can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Description")), hold.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("IsVoid")), hold.IsVoid },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("AppearsOnStatementAs")), hold.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Meta")), hold.Meta == null ? new JObject() : JToken.FromObject(hold.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<HoldList>(BalancedHttpRest.Put(string.Format("{0}", hold.Href), parameters));
        }

        public DebitList Capture(Hold hold)
        {
            if (hold == null) throw new ArgumentNullException("hold", "Hold can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Amount")), hold.Amount },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Description")), hold.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("AppearsOnStatementAs")), hold.AppearsOnStatementAs },
            };

            return BalancedJsonSerializer.DeSerialize<DebitList>(BalancedHttpRest.Post(string.Format("{0}", hold.Href), parameters));
        }

        public HoldList Void(Hold hold)
        {
            if (hold == null) throw new ArgumentNullException("hold", "Hold can not be null");
            if (string.IsNullOrEmpty(hold.Href)) throw new ArgumentNullException("hold", "Hold Uri can not be null");

            var parameters = new JObject
            {
                 { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Description")), hold.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("IsVoid")), true },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("AppearsOnStatementAs")), hold.AppearsOnStatementAs },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Hold).GetProperty("Meta")), hold.Meta == null ? new JObject() : JToken.FromObject(hold.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<HoldList>(BalancedHttpRest.Put(string.Format("{0}", hold.Href), parameters));
        }
    }
}
