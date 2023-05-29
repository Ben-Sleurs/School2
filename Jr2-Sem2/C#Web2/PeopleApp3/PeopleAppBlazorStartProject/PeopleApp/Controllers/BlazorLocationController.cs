using Microsoft.AspNetCore.Mvc;

namespace PeopleApp.Controllers;

public class BlazorLocationController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("_BlazorServer_Host");
    }
}