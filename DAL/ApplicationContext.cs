using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DAL.Models;
using System.IO;

namespace DAL
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
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // var config = GetConfig();
            // var connectionString = config.GetConnectionString("Default");
            //
            // optionsBuilder.UseSqlServer(connectionString);
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
