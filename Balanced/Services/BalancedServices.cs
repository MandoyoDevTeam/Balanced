using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{   
    public abstract class BalancedServices<T> where T : BalancedObject
    {
        public abstract string RootUri { get; }

        protected BalancedRest BalancedHttpRest { get; private set; }

        protected BalancedServices(string secret)
        {
            if (String.IsNullOrEmpty(secret)) throw new ArgumentNullException("secret");
            BalancedHttpRest = new BalancedRest(secret);
        }

        protected T Create(T item)
        {
            var parameters = (JObject)JToken.FromObject(item);
            
            return BalancedJsonSerializer.DeSerialize<T>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        protected T Get(T item)
        {
            if(item == null) throw new ArgumentNullException("item", "Item can not be null");
            if (string.IsNullOrEmpty(item.Uri) && string.IsNullOrEmpty(item.Id)) throw new ArgumentNullException("item", "Item Uri can not be null");
            
            var uri = string.IsNullOrEmpty(item.Uri) ? string.Format("{0}/{1}", RootUri, item.Id) : item.Uri;
            
            return BalancedJsonSerializer.DeSerialize<T>(BalancedHttpRest.Get(uri));
        }

        protected PagedList<T> List(int limit = 10, int offset = 0)
        {
            var parameters = new NameValueCollection { { "limit", limit.ToString(CultureInfo.InvariantCulture) }, { "offset", offset.ToString(CultureInfo.InvariantCulture) } };
            return BalancedJsonSerializer.DeSerialize<PagedList<T>>(BalancedHttpRest.Get(RootUri, parameters));
        }

        protected T Update(T item)
        {
            var parameters = (JObject)JToken.FromObject(item);

            return BalancedJsonSerializer.DeSerialize<T>(BalancedHttpRest.Put(string.Format("{0}", item.Uri), parameters));

        }

        protected bool Delete(T item)
        {
            if (item == null) throw new ArgumentNullException("item", "Item can not be null");
            if (string.IsNullOrEmpty(item.Uri) && string.IsNullOrEmpty(item.Id)) throw new ArgumentNullException("item", "Item Uri can not be null");

            var uri = string.IsNullOrEmpty(item.Uri) ? string.Format("{0}/{1}", RootUri, item.Id) : item.Uri;

            BalancedHttpRest.Delete(string.Format("{0}", uri));

            return true;
        }
    }
}
