using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models.Data;
using HogeschoolPXL.Data.DefaultData;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Identity;

namespace HogeschoolPXL.Controllers
{
    [Authorize(Roles= Roles.Admin)]
    public class GebruikersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GebruikersController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Gebruikers
        public async Task<IActionResult> Index()
        {
            var roles = new List<string>{ Roles.Admin, Roles.Student, Roles.Lector };
            var gebruikersMetConfirmedRol = await _context.Gebruiker
                .Where(x => roles.Contains(x.Role))
                .ToListAsync();
              return View(gebruikersMetConfirmedRol);
        }
        public async Task<IActionResult> ConfirmUsers()
        {
            var tempRoles = new List<string> { Roles.TempAdmin, Roles.TempStudent, Roles.TempLector };
            var gebruikersMetTempRol = await _context.Gebruiker
                .Where(x => tempRoles.Contains(x.Role))
                .ToListAsync();
            return View(gebruikersMetTempRol);
        }
        public async Task<IActionResult> ConfirmUser(int? id)
        {
            var gebruiker = await _context.Gebruiker.FindAsync(id);
            gebruiker.Role = gebruiker.Role.Substring(4);
            _context.Gebruiker.Update(gebruiker);
            if (gebruiker.Role=="Student")
            {
                var student = new Student { GebruikerId = gebruiker.GebruikerId };
                _context.Student.Add(student);
            }
            if (gebruiker.Role=="Lector")
            {
                var lector = new Lector { GebruikerId = gebruiker.GebruikerId };
                _context.Lector.Add(lector);
            }
            _context.SaveChanges();
            var user = await _userManager.FindByEmailAsync(gebruiker.Email);
            var result = await _userManager.AddToRoleAsync(user, gebruiker.Role);
            return RedirectToAction(nameof(Index));

        }

        // GET: Gebruikers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gebruiker == null)
            {
                return NotFound();
            }

            var gebruiker = await _context.Gebruiker
                .FirstOrDefaultAsync(m => m.GebruikerId == id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            return View(gebruiker);
        }

        // GET: Gebruikers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gebruikers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GebruikerId,Naam,Voornaam,Email")] Gebruiker gebruiker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gebruiker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gebruiker);
        }

        // GET: Gebruikers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gebruiker == null)
            {
                return NotFound();
            }

            var gebruiker = await _context.Gebruiker.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }
            return View(gebruiker);
        }

        // POST: Gebruikers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("GebruikerId,Naam,Voornaam,Email")] Gebruiker gebruiker)
        {
            if (id != gebruiker.GebruikerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gebruiker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GebruikerExists(gebruiker.GebruikerId))
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
            return View(gebruiker);
        }

        // GET: Gebruikers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gebruiker == null)
            {
                return NotFound();
            }

            var gebruiker = await _context.Gebruiker
                .FirstOrDefaultAsync(m => m.GebruikerId == id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            return View(gebruiker);
        }

        // POST: Gebruikers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Gebruiker == null)
            {
                return Problem("Entity set 'AppDbContext.Gebruiker'  is null.");
            }
            var gebruiker = await _context.Gebruiker.FindAsync(id);
            if (gebruiker != null)
            {
                _context.Gebruiker.Remove(gebruiker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GebruikerExists(int? id)
        {
          return _context.Gebruiker.Any(e => e.GebruikerId == id);
        }
    }
}
