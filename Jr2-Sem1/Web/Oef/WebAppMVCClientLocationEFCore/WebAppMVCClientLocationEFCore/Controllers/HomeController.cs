using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppMVCClientLocationEFCore.Data;
using WebAppMVCClientLocationEFCore.Models;

namespace WebAppMVCClientLocationEFCore.Controllers
{
    public class HomeController : Controller
    {
        private ClientLocationContext _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController([FromServices] ClientLocationContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            int[] arr = new int[2] { _context.Clients.Count(), _context.Locations.Count() };
            return View(arr);
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