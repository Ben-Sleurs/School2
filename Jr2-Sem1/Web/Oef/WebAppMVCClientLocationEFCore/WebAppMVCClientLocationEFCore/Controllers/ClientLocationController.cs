using Microsoft.AspNetCore.Mvc;
using WebAppMVCClientLocationEFCore.Data;
using WebAppMVCClientLocationEFCore.Models;

namespace WebAppMVCClientLocationEFCore.Controllers
{
    public class ClientLocationController : Controller
    {
        private ClientLocationContext _context;
        private IWebHostEnvironment _environment;
        public ClientLocationController([FromServices] ClientLocationContext context, [FromServices] IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var clients = _context.Clients;
            var locations = _context.Locations;
            var clientLocations = clients.Select(x => new ClientLocation
            (
                x.ClientName,
                locations.Where( location => location.LocationId==x.LocationId).FirstOrDefault().City

            ));
            return View(clientLocations);
        }
    }
}
