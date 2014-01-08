using System;
using System.Collections.Specialized;
using System.Globalization;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public abstract class BalancedServices<T, TList>
        where T : BalancedObject
        where TList : BalancedList
    {
        public abstract string RootUri { get; }

        protected BalancedRest BalancedHttpRest { get; private set; }

        protected BalancedServices()
        {
            BalancedHttpRest = new BalancedRest();
        }

        protected TList Create(T item)
        {
            var parameters = (JObject)JToken.FromObject(item);

            return BalancedJsonSerializer.DeSerialize<TList>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        protected TList Get(T item)
        {
            if (item == null) throw new ArgumentNullException("item", "Item can not be null");
            if (string.IsNullOrEmpty(item.Href) && string.IsNullOrEmpty(item.Id)) throw new ArgumentNullException("item", "Item Uri can not be null");

            var uri = string.IsNullOrEmpty(item.Href) ? string.Format("{0}/{1}", RootUri, item.Id) : item.Href;

            return BalancedJsonSerializer.DeSerialize<TList>(BalancedHttpRest.Get(uri));
        }

        public TList List(int limit = 10, int offset = 0)
        {
            var parameters = new NameValueCollection
            {
                { "limit", limit.ToString(CultureInfo.InvariantCulture) }, 
                { "offset", offset.ToString(CultureInfo.InvariantCulture) }
            };

            return BalancedJsonSerializer.DeSerialize<TList>(BalancedHttpRest.Get(RootUri, parameters));

        }

        protected TList Update(T item)
        {
            var parameters = (JObject)JToken.FromObject(item);

            return BalancedJsonSerializer.DeSerialize<TList>(BalancedHttpRest.Put(string.Format("{0}", item.Href), parameters));

        }

        protected bool Delete(T item)
        {
            if (item == null) throw new ArgumentNullException("item", "Item can not be null");
            if (string.IsNullOrEmpty(item.Href) && string.IsNullOrEmpty(item.Id)) throw new ArgumentNullException("item", "Item Uri can not be null");

            var uri = string.IsNullOrEmpty(item.Href) ? string.Format("{0}/{1}", RootUri, item.Id) : item.Href;

            BalancedHttpRest.Delete(string.Format("{0}", uri));

            return true;
        }
    }
}
