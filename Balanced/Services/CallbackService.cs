using System;
using Balanced.Entities;
using Balanced.Helpers;
using Balanced.Structs;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class CallbackService : BalancedServices<Callback, CallbackList>
    {

        public override string RootUri
        {
            get
            {
                return string.Format("/callbacks");
            }
        }

        public new CallbackList Create(Callback callback)
        {
            if (callback == null) throw new ArgumentNullException("callback", "Callback can not be null");
            if (string.IsNullOrEmpty(callback.Url)) throw new ArgumentNullException("callback", "Callback Url can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Callback).GetProperty("Url")), callback.Url },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Callback).GetProperty("Method")), BalancedAttributeHelper.GetEnumAttributes(typeof(CallbackMethod).GetMember(callback.Method.ToString())) },
            };

            return BalancedJsonSerializer.DeSerialize<CallbackList>(BalancedHttpRest.Post(string.Format("{0}", RootUri), parameters));
        }

        public new CallbackList Get(Callback callback)
        {
            return base.Get(callback);
        }

        public new CallbackList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new bool Delete(Callback callback)
        {
            return base.Delete(callback);
        }
    }
}
