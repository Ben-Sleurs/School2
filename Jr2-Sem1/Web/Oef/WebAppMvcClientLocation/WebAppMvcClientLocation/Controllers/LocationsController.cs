using Microsoft.AspNetCore.Mvc;
using WebAppMvcClientLocation.Data;
using WebAppMvcClientLocation.Models;

namespace WebAppMvcClientLocation.Controllers
{
    public class LocationsController : Controller
    {
        public IActionResult Index()
        {
            return View(Database.Locations);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateLocation(Location locatie)
        {
            if (ModelState.IsValid)
            {
                int? newId = Database.Locations.Max(x => x.LocationId) + 1;
                locatie.LocationId = newId;
                Database.AddLocation(locatie);
                return RedirectToAction("Index", "Locations");
            }
            return View("Create", locatie);
            
        }
    }
}
