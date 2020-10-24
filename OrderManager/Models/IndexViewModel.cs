using System.Collections.Generic;
using System.Composition.Convention;
using System.Linq;
using DAL;
using DAL.Models;

namespace OrderManager.Models
{
    public class IndexViewModel
    {
        public static IndexViewModel Create(IReadOnlyCollection<Order> orders, IReadOnlyCollection<Order> sortedOrders, IReadOnlyCollection<Provider> providers, IReadOnlyCollection<OrderItem> orderItems)
        {
            var model = new IndexViewModel
            {
                Orders = orders,
                SortedOrders = sortedOrders,
                Providers = providers,
                OrderItems = orderItems
            };

            return model;
        }

        public IReadOnlyCollection<Order> Orders { get; set; }
        public IReadOnlyCollection<Order> SortedOrders { get; set; }
        public IReadOnlyCollection<Provider> Providers { get; set; }
        public IReadOnlyCollection<OrderItem> OrderItems { get; set; }
    }
}