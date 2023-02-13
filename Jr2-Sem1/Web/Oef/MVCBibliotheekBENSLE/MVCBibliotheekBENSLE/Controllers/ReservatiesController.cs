using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBibliotheekBENSLE.Data;
using MVCBibliotheekBENSLE.Data.DefaultData;
using MVCBibliotheekBENSLE.Models.Data;

namespace MVCBibliotheekBENSLE.Controllers
{
    public class ReservatiesController : Controller
    {
        AppDbContext _context;
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public ReservatiesController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Reservaties
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reservatie.Include(r => r.Gebruiker).Include(r => r.VergaderZaal);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reservaties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservatie == null)
            {
                return NotFound();
            }

            var reservatie = await _context.Reservatie
                .Include(r => r.Gebruiker)
                .Include(r => r.VergaderZaal)
                .FirstOrDefaultAsync(m => m.ReservatieId == id);
            if (reservatie == null)
            {
                return NotFound();
            }

            return View(reservatie);
        }
        [Authorize(Roles = Roles.BibGebruiker)]
        // GET: Reservaties/Create
        public IActionResult Create()
        {
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "Achternaam");
            ViewData["VergaderZaalId"] = new SelectList(_context.VergaderZaal, "VergaderZaalId", "Naam");
            return View();
        }

        // POST: Reservaties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservatieId,VergaderZaalId,GebruikerId,Datum")] Reservatie reservatie)
        {
            //zoiets als dit zou het moeten zijn maar programma werkt niet meer na ik dit uitvoer
            //var id = _userManager.GetUserId(User);
            //var gebruiker = _context.Gebruiker.Where(x => x.IdentityUserId == id).SingleOrDefault();
            //reservatie.GebruikerId = gebruiker.GebruikerId;
            if (AlGerserveerd(reservatie))
            {
                // Hier komt custom view met error, geen tijd meer
                return View(reservatie);
            }
            if (ModelState.IsValid)
            {
                _context.Add(reservatie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "Achternaam", reservatie.GebruikerId);
            ViewData["VergaderZaalId"] = new SelectList(_context.VergaderZaal, "VergaderZaalId", "Naam", reservatie.VergaderZaalId);
            return View(reservatie);
        }

        private bool AlGerserveerd(Reservatie reservatie)
        {
            var dag = reservatie.Datum.Value;
            return _context.Reservatie.Any(x => x.Datum.Value.Year == dag.Year && x.Datum.Value.Month == dag.Month && x.Datum.Value.Day == dag.Day && x.VergaderZaal == reservatie.VergaderZaal);
        }

        // GET: Reservaties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservatie == null)
            {
                return NotFound();
            }

            var reservatie = await _context.Reservatie.FindAsync(id);
            if (reservatie == null)
            {
                return NotFound();
            }
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "Achternaam", reservatie.GebruikerId);
            ViewData["VergaderZaalId"] = new SelectList(_context.VergaderZaal, "VergaderZaalId", "Naam", reservatie.VergaderZaalId);
            return View(reservatie);
        }

        // POST: Reservaties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ReservatieId,VergaderZaalId,GebruikerId,Datum")] Reservatie reservatie)
        {
            if (id != reservatie.ReservatieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservatie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservatieExists(reservatie.ReservatieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "Achternaam", reservatie.GebruikerId);
            ViewData["VergaderZaalId"] = new SelectList(_context.VergaderZaal, "VergaderZaalId", "Naam", reservatie.VergaderZaalId);
            return View(reservatie);
        }

        // GET: Reservaties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservatie == null)
            {
                return NotFound();
            }

            var reservatie = await _context.Reservatie
                .Include(r => r.Gebruiker)
                .Include(r => r.VergaderZaal)
                .FirstOrDefaultAsync(m => m.ReservatieId == id);
            if (reservatie == null)
            {
                return NotFound();
            }

            return View(reservatie);
        }

        // POST: Reservaties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Reservatie == null)
            {
                return Problem("Entity set 'AppDbContext.Reservatie'  is null.");
            }
            var reservatie = await _context.Reservatie.FindAsync(id);
            if (reservatie != null)
            {
                _context.Reservatie.Remove(reservatie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservatieExists(int? id)
        {
          return _context.Reservatie.Any(e => e.ReservatieId == id);
        }
    }
}
