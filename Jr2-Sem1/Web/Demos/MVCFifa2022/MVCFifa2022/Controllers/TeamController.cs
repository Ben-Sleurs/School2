using Microsoft.AspNetCore.Mvc;
using MVCFifa2022.Data;
using MVCFifa2022.Models;

namespace MVCFifa2022.Controllers
{
    public class TeamController : Controller
    {
        ApplicationDbContext _context;
        public TeamController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var teams = _context.Teams;
            return View(teams);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }
            _context.Teams.Add(team);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
