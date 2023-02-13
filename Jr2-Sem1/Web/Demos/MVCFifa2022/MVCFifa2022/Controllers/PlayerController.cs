using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCFifa2022.Data;
using MVCFifa2022.Models;

namespace MVCFifa2022.Controllers
{
    public class PlayerController : Controller
    {
        private ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        public PlayerController([FromServices] ApplicationDbContext context, [FromServices] IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var players = _context.Players;
            return View(players);
        }
        [HttpGet]
        public IActionResult Create2()
        {
            NewPlayer player = new NewPlayer();
            ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamName");
            return View(player);
        }
        [HttpPost]
        public IActionResult Create2(NewPlayer player)
        {
            if (!ModelState.IsValid)
            {
                return View(player);
            }
            AddNewPlayer(player);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            Player player = new Player();
            return View(player);
        }
        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                AddPlayer(player);
                return RedirectToAction("Index");
            }
            return View(player);
        }
        public IActionResult Details(int id)
        {
            var player = _context.Players.Where(x => x.PlayerId == id).FirstOrDefault();
            var path = _environment.WebRootPath;
            var fileExist = false;
            if (player.ImageLink != null)
            {
                var file = Path.Combine($"{path}\\images", player.ImageLink);
                fileExist = System.IO.File.Exists(file);
            }
            ViewBag.FileExist = fileExist;
            return View(player);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var player = _context.Players.Where(x => x.PlayerId == id).FirstOrDefault();
            return View(player);
        }
        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            var player = _context.Players.Where(x => x.PlayerId == id).FirstOrDefault();
            _context.Players.Remove(player);
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var player = _context.Players.Where(x => x.PlayerId == id).FirstOrDefault();
            return View(player);
        }
        [HttpPost]
        public IActionResult Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                UpdatePlayer(player);
                return RedirectToAction("Index");
            }
            return View(player);
        }
        private void AddPlayer(Player player)
        {

            _context.Players.Add(player);
            _context.SaveChanges();

        }
        private void AddNewPlayer(NewPlayer player)
        {
            Player p = (Player)player;
            _context.Players.Add(player);
            _context.SaveChanges();

            var tp = new TeamPlayer();
            tp.PlayerId = p.PlayerId;
            tp.TeamId = player.TeamId;
            tp.StartDate = DateTime.Now;
            _context.TeamPlayer.Add(tp);
            _context.SaveChanges();

        }

        private void UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
            _context.SaveChanges();
        }
        
    }
}
