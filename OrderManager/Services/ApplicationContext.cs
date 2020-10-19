using Microsoft.EntityFrameworkCore;
using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManager.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }
    }
}
