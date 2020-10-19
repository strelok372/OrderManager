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
            return View(Context.Orders);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateEdit()
        {
            return View();
        }

        public void Delete()
        {            
        }

        public IActionResult Show()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
