using Microsoft.AspNetCore.Mvc;
using MVCEndpointRouting.Models;
using System.Diagnostics;
using MVCEndpointRouting.Models.ViewModels;
using MVCEndpointRouting.Services;

namespace MVCEndpointRouting.Controllers
{
    public class HomeController : Controller
    {
        private IRoutingRepository _routingRepo;

        public HomeController(IRoutingRepository routingRepo)
        {
            _routingRepo = routingRepo;
        }

        public IActionResult Index()
        {
            RoutingModel routingModel = new RoutingModel("Home");
            routingModel.ActionName = "Index";
            _routingRepo.Add(routingModel);
            return View();
        }

        public IActionResult RoutingList()
        {
            RoutingModel routingModel = new RoutingModel("Home");
            routingModel.ActionName = "RoutingList";
            _routingRepo.Add(routingModel);
            return View(_routingRepo.RoutingModels);
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