using Microsoft.AspNetCore.Mvc;
using MVCWebApp.Models;

namespace MVCWebApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TestViewbag()
        {
            ViewBag.Teller = 3;
            ViewData["Teller"] = 3;
            return View();
        }
        public IActionResult Formulier()
        {
            return View();
        }
        public IActionResult FormulierPost()
        {
            string invoer = Request.Form["TestInvoer"];
            TestModel model = new TestModel();
            model.TestInvoer = invoer;
            return View(model);
        }
    }
}
