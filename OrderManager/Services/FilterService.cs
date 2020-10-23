using OrderManager.Entities;
using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManager.Services
{
    public class FilterService
    {
        public static List<Order> Filter(Filters filters, ApplicationContext context)
        {
            var orders = GetFilteredOrders(filters, context);

            FilterByProviders(filters, orders, context);

            FilterByOrderItems(filters, context, orders);

            return orders;
        }

        private static List<Order> GetFilteredOrders(Filters filters, ApplicationContext context)
        {
            var od = filters.OrderDatesId;
            var on = filters.OrderNumbersId;

            IQueryable<Order> result = context.Orders;

            if (od != null)
                result = result.Where(order => od.Contains(order.Id));

            if (on != null)
                result = result.Where(order => on.Contains(order.Id));

            return result.ToList();
        }

        private static void FilterByOrderItems(Filters filters, ApplicationContext context, List<Order> resultOrders)
        {
            var oin = filters.OrderItemNames;
            var oiu = filters.OrderItemUnits;

            IQueryable<OrderItem> orderItems = context.OrderItems;

            if (oin != null)
                orderItems = orderItems.Where(orderItem => oin.Contains(orderItem.Name));

            if (oiu != null)
                orderItems = orderItems.Where(orderItem => oiu.Contains(orderItem.Unit));

            var orderItemsIds = orderItems
                .Select(oi => oi.OrderId)
                .ToArray();

            if (orderItemsIds.Length > 0)
            {
                resultOrders.RemoveAll(ord => !orderItemsIds.Contains(ord.Id));
            }
        }

        private static void FilterByProviders(Filters filters, List<Order> resultOrders, ApplicationContext context)
        {
            var pn = filters.ProviderNames;

            if (pn != null)
            {
                var providers = context.Providers.ToList(); 
                resultOrders.RemoveAll(ord => !pn.Contains(ord.Provider.Name));
            }
        }
    }
}
