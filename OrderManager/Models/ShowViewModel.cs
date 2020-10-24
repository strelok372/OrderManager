using DAL.Models;
using System;
using System.Collections.Generic;

namespace OrderManager.Models
{
    public class ShowViewModel
    {
        public static ShowViewModel Create(Order order, IReadOnlyCollection<OrderItem> orderItems)
        {
            return new ShowViewModel()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                Provider = order.Provider.Name,
                OrderItems = orderItems
            };
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Provider { get; set; }
        public IReadOnlyCollection<OrderItem> OrderItems { get; set; }
    }
}
