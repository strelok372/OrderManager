using BLL.Models;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IOrderService
    {
        List<Order> Filter(Filters filters);
        Task<List<Order>> GetAll(CancellationToken cancellationToken);
        Task<Order> Get(int? id, CancellationToken cancellationToken);
        void Add(Order order, CancellationToken cancellationToken);
        void Update(Order order, CancellationToken cancellationToken);
    }

    public partial class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;

        public OrderService(ApplicationContext context)
        {
            _context = context;
        }

        public Task<List<Order>> GetAll(CancellationToken cancellationToken)
        {            
            return _context.Orders.Include(order => order.Provider).ToListAsync(cancellationToken);
        }

        public Task<Order> Get(int? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return null;
            }

            return _context.Orders.Include(order => order.Provider).FirstAsync(it => it.Id == id, cancellationToken);

        }

        public List<Order> Filter(Filters filters)
        {
            return FilterOrders(filters);
        }


        public void Update(Order order, CancellationToken cancellationToken)
        {
            if (order != null)
                _context.Update(order);
        }

        public void Add(Order order, CancellationToken cancellationToken)
        {
            if (order != null)
                _context.Add(order);
        }
    }

    public partial class OrderService
    {
        public List<Order> FilterOrders(Filters filters)
        {
            var orders = GetFilteredOrders(filters);

            FilterByProviders(filters, orders);

            FilterByOrderItems(filters, orders);

            return orders;
        }

        private List<Order> GetFilteredOrders(Filters filters)
        {
            var od = filters.OrderDatesId;
            var on = filters.OrderNumbersId;

            IQueryable<Order> result = _context.Orders;

            if (od != null)
                result = result.Where(order => od.Contains(order.Id));

            if (on != null)
                result = result.Where(order => on.Contains(order.Id));

            return result.ToList();
        }

        private void FilterByOrderItems(Filters filters, List<Order> resultOrders)
        {
            var oin = filters.OrderItemNames;
            var oiu = filters.OrderItemUnits;

            IQueryable<OrderItem> orderItems = _context.OrderItems;

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

        private void FilterByProviders(Filters filters, List<Order> resultOrders)
        {
            var pn = filters.ProviderNames;

            if (pn != null)
            {
                //var providers = _context.Providers.ToList();
                resultOrders.RemoveAll(ord => !pn.Contains(ord.Provider.Name));
            }
        }
    }
}