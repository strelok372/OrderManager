using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLL.Service;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        private readonly ApplicationContext _context;
        private readonly IOrderService _orderService;
        private readonly Mock<ILogService> _logServiceMock;

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            _context = new ApplicationContext(options);
            _logServiceMock = new Mock<ILogService>();

            _logServiceMock
                .Setup(it => it.Get())
                .Returns("66");
            
            _orderService = new OrderService(_context, _logServiceMock.Object);
        }
        
        [Fact]
        public async Task Test1()
        {
            var order = new Order()
            {
                Number = "42"
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var result = await _orderService.GetAll(CancellationToken.None);
            
            Assert.NotNull(result);
            Assert.Equal("42", result.FirstOrDefault().Number);

        }
    }
}