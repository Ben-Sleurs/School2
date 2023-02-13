using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using WebAppMVCClientLocationEFCore.Data;
using WebAppMVCClientLocationEFCore.Models;

namespace WebAppMVCClientLocationEFCore.Controllers
{
    public class ClientController : Controller
    {
        private ClientLocationContext _context;
        private IWebHostEnvironment _environment;
        public ClientController([FromServices] ClientLocationContext context, [FromServices] IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var clients = _context.Clients;
            return View(clients);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Client client = new Client();
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "City");
            return View(client);
        }
        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                AddClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }
        private void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
    }
}
