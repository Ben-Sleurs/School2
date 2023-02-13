using Microsoft.AspNetCore.Mvc;
using WebAppMvcClientLocation.Data;
using WebAppMvcClientLocation.Models;

namespace WebAppMvcClientLocation.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Index()
        {
            return View(Database.Clients);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateKlant(Client klant)
        {
            if (ModelState.IsValid)
            {
                if (klant.ClientId < 1)
                {
                    ModelState.AddModelError("", "Id moet groter dan 1 zijn");
                    return View("Create", klant);
                }
                else if (Database.Clients.Select(x => x.ClientId).ToList().Contains(klant.ClientId))
                {
                    ModelState.AddModelError("", "Id bestaat al");
                    return View("Create", klant);
                }
                Database.AddClient(klant);
                return RedirectToAction("Index", "Clients");
            }
            return View("Create", klant);
        }
    }
}
