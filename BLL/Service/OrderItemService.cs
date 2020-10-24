using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Service
{
    public interface IOrderItemService
    {
        Task<IReadOnlyCollection<OrderItem>> GetAll(CancellationToken cancellationToken);
        Task<IReadOnlyCollection<OrderItem>> GetAllForOrder(int? id, CancellationToken cancellationToken);
    }

    public class OrderItemService : IOrderItemService
    {
        public ApplicationContext ApplicationContext { get; }

        public OrderItemService(ApplicationContext applicationContext)
        {
            ApplicationContext = applicationContext;
        }

        public async Task<IReadOnlyCollection<OrderItem>> GetAll(CancellationToken cancellationToken)
        {
            return await ApplicationContext.OrderItems.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<OrderItem>> GetAllForOrder(int? id, CancellationToken cancellationToken)
        {
            return await ApplicationContext.OrderItems.Where(item => item.OrderId == id).ToListAsync();
        }
    }
}
