using Microsoft.AspNetCore.Mvc;

namespace MVCEndpointRouting.Controllers;

public class RoutingController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}