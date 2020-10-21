using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManager.Services
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }
            
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = GetConfig();
            var connectionString = config.GetConnectionString("Default");

            optionsBuilder.UseSqlServer(connectionString);
        }

        private static IConfigurationRoot GetConfig()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            return config;
        }
    }
}
