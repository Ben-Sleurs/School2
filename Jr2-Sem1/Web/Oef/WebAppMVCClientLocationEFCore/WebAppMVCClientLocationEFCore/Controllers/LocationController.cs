using Microsoft.AspNetCore.Mvc;
using WebAppMVCClientLocationEFCore.Data;
using WebAppMVCClientLocationEFCore.Models;

namespace WebAppMVCClientLocationEFCore.Controllers
{
    public class LocationController : Controller
    {
        private ClientLocationContext _context;
        private IWebHostEnvironment _environment;
        public LocationController([FromServices] ClientLocationContext context, [FromServices] IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var locations = _context.Locations;
            return View(locations);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Location location = new Location();
            return View(location);
        }
        [HttpPost]
        public IActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                AddLocation(location);
                return RedirectToAction("Index");
            }
            return View(location);
        }
        private void AddLocation(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }
    }
}
