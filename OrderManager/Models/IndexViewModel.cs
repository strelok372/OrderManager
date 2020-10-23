using System.Collections.Generic;
using System.Composition.Convention;
using System.Linq;
using DAL;
using DAL.Models;

namespace OrderManager.Models
{
    public class IndexViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Order> SortedOrders { get; set; }
        public List<Provider> Providers { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public static IndexViewModel Create(List<Order> sortedOrders, ApplicationContext context)
        {
            var model = new IndexViewModel
            {
                Orders = context.Orders.ToList(),
                Providers = context.Providers.ToList(),
                OrderItems = context.OrderItems.ToList(),
                SortedOrders = sortedOrders
            };
            return model;
        }
    }
}