using OrderManager.Models;
using System;

namespace OrderManager.Entities
{
    public class DetailedOrder
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Provider { get; set; }
        public OrderItem[] OrderItems { get; set; }
    }
}
