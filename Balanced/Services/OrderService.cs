using System;
using Balanced.Entities;
using Balanced.Helpers;
using Newtonsoft.Json.Linq;

namespace Balanced.Services
{
    public class OrderService : BalancedServices<Order, OrderList>
    {
        public override string RootUri
        {
            get
            {
                return string.Format("/orders");
            }
        }

        public OrderList Create(Order order, Customer customer)
        {
            if (order == null) throw new ArgumentNullException("order", "Order can not be null");

            if (customer == null) throw new ArgumentNullException("customer", "Customer can not be null");
            if (string.IsNullOrEmpty(customer.Href)) throw new ArgumentNullException("customer", "Customer Href can not be null");
            
            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Order).GetProperty("Description")), order.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Order).GetProperty("Meta")), order.Meta == null ? new JObject() : JToken.FromObject(order.Meta) },                
            };

            return BalancedJsonSerializer.DeSerialize<OrderList>(BalancedHttpRest.Post(string.Format("{0}{1}", customer.Href, RootUri), parameters));
        }

        public new OrderList Get(Order order)
        {
            return base.Get(order);
        }

        public new OrderList List(int limit = 10, int offset = 0)
        {
            return base.List(limit, offset);
        }

        public new OrderList Update(Order order)
        {
            if (order == null) throw new ArgumentNullException("order", "Order can not be null");
            if (string.IsNullOrEmpty(order.Href)) throw new ArgumentNullException("order", "Order Id can not be null");

            var parameters = new JObject
            {
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Order).GetProperty("Description")), order.Description },
                { BalancedAttributeHelper.GetPropertyAttributes(typeof(Order).GetProperty("Meta")), order.Meta == null ? new JObject() : JToken.FromObject(order.Meta) },
            };

            return BalancedJsonSerializer.DeSerialize<OrderList>(BalancedHttpRest.Put(string.Format("{0}", order.Href), parameters));
        }
    }
}
