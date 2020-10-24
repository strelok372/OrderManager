using BLL.Models;
using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderManager.Models;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> Logger { get; }
        private IOrderService OrderService { get; }
        private IProviderService ProviderService { get; }
        private IOrderItemService OrderItemsService { get; }

        public HomeController(ILogger<HomeController> logger, IOrderService orderService, IProviderService providerService, IOrderItemService orderItemsService)
        {
            Logger = logger;
            OrderService = orderService;
            ProviderService = providerService;
            OrderItemsService = orderItemsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var orders = await OrderService.GetAll(cancellationToken);
            var providers = await ProviderService.GetAll(cancellationToken);
            var orderItems = await OrderItemsService.GetAll(cancellationToken);

            var model = IndexViewModel.Create(orders, orders, providers, orderItems);
            return base.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Filters filters, CancellationToken cancellationToken)
        {
            var orders = await OrderService.GetAll(cancellationToken);
            var providers = await ProviderService.GetAll(cancellationToken);
            var orderItems = await OrderItemsService.GetAll(cancellationToken);
            var sortedOrders = OrderService.Filter(filters);

            var model = IndexViewModel.Create(sortedOrders, orders, providers, orderItems);
            
            return base.View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            var order = await OrderService.Get(id, cancellationToken);
            var orderItems = await OrderItemsService.GetAllForOrder(order.Id, cancellationToken);
            var showModel = ShowViewModel.Create(order, orderItems);
            ViewBag.Providers = await ProviderService.GetAll(cancellationToken);
            return View(showModel);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            OrderService.Update(order, CancellationToken.None);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(Order order)
        {
            OrderService.Add(order, CancellationToken.None);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult Add()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            return id == null ? RedirectToAction("Index") : null;
        }

        public async Task<IActionResult> Show(int? id, CancellationToken cancellationToken)
        {
            if (id == null) return RedirectToAction("Index");
            var order = await OrderService.Get(id, cancellationToken);
            var orderItems = await OrderItemsService.GetAllForOrder(order.Id, cancellationToken);
            var showModel = ShowViewModel.Create(order, orderItems);
            return View(showModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
