using Microsoft.AspNetCore.Mvc;

namespace HogeschoolPXL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
