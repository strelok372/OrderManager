using OrderManager.Models;
using System;
using System.Collections.Generic;
using DAL.Models;

namespace OrderManager.Entities
{
    public class DetailedOrder
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string Provider { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
