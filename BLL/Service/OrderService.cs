using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLL.Models;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BLL.Service
{
    public interface IOrderService
    {
        List<Order> Filter(Filters filters);
        Task<List<Order>> GetAll(CancellationToken cancellationToken);
        Task<Order> Get(int? id, CancellationToken cancellationToken);
    }

    public class OrderService : IOrderService
    {
        private readonly ApplicationContext _context;
        private readonly ILogService _logService;

        public OrderService(ApplicationContext context, ILogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public List<Order> Filter(Filters filters)
        {
            var orders = GetFilteredOrders(filters);

            FilterByProviders(filters, orders);

            FilterByOrderItems(filters, orders);

            return orders;
        }

        public Task<List<Order>> GetAll(CancellationToken cancellationToken)
        {
            var data = _logService.Get();
            
            return _context.Orders.ToListAsync(cancellationToken);
        }

        public Task<Order> Get(int? id, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return null;
            }

            return _context.Orders.FirstAsync(it => it.Id == id, cancellationToken);

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
                var providers = _context.Providers.ToList(); 
                resultOrders.RemoveAll(ord => !pn.Contains(ord.Provider.Name));
            }
        }
    }
}