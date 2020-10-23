using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLL.Models;
using BLL.Service;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderManager.Entities;
using OrderManager.Models;

namespace OrderManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrderService _orderService;
        private readonly IProviderService _providerService;

        public HomeController(ILogger<HomeController> logger, IOrderService orderService, IProviderService providerService)
        {
            _logger = logger;
            _orderService = orderService;
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAll(cancellationToken);
            var model = IndexViewModel.Create(orders, null);
            return base.View(model);
        }

        [HttpPost]
        public IActionResult Index(Filters filters)
        {
            var orders = _orderService.Filter(filters);
            _providerService.GetAll(CancellationToken.None);
            var model = IndexViewModel.Create(orders, null);
            
            return base.View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            var model = await _orderService.Get(id, cancellationToken);
            ViewBag.Providers = await _providerService.GetAll(cancellationToken);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            // Context.Orders.Update(order);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(Order order)
        {
            // if (order != null) Context.Orders.Add(order);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            return null;
        }

        public async Task<IActionResult> Show(int? id, CancellationToken cancellationToken)
        {
            if (id == null) return null;
            await _orderService.Get(id, CancellationToken.None);
            var order = await _orderService.Get(id, cancellationToken);
            return View(order);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
