using Microsoft.AspNetCore.Mvc;
using MVCDependencyInjection.Models;
using MVCDependencyInjection.Services;
using System.Diagnostics;

namespace MVCDependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductRepository _repo;

        public HomeController(ILogger<HomeController> logger, IProductRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public IActionResult Index()
        {
            ViewBag.ProductCount = _repo.Products.Count();
            return View();
        }

        public IActionResult Privacy()
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