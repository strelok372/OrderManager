using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderManager.Models;
using OrderManager.Services;

namespace OrderManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ApplicationContext Context { get; }

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            return base.View(Context);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var model = id.HasValue ? Context.Orders.First(x => x.Id == id) : null;
            ViewBag.Providers = Context.Providers.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            Context.Orders.Update(order);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(Order order)
        {
            if (order != null) Context.Orders.Add(order);
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

        public IActionResult Show(int? id)
        {
            if (id == null) return null;
            return View(Context.Orders.First(x => x.Id == id));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
