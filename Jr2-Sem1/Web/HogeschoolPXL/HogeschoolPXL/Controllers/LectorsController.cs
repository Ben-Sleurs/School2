using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models.Data;

namespace HogeschoolPXL.Controllers
{
    public class LectorsController : Controller
    {
        private readonly AppDbContext _context;

        public LectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Lectors
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Lector.Include(l => l.Gebruiker);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Lectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lector == null)
            {
                return NotFound();
            }

            var lector = await _context.Lector
                .Include(l => l.Gebruiker)
                .FirstOrDefaultAsync(m => m.LectorId == id);
            if (lector == null)
            {
                return NotFound();
            }

            return View(lector);
        }

        // GET: Lectors/Create
        public IActionResult Create()
        {
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "GebruikerId");
            return View();
        }

        // POST: Lectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LectorId,GebruikerId")] Lector lector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "GebruikerId", lector.GebruikerId);
            return View(lector);
        }

        // GET: Lectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lector == null)
            {
                return NotFound();
            }

            var lector = await _context.Lector.FindAsync(id);
            if (lector == null)
            {
                return NotFound();
            }
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "GebruikerId", lector.GebruikerId);
            return View(lector);
        }

        // POST: Lectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LectorId,GebruikerId")] Lector lector)
        {
            if (id != lector.LectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectorExists(lector.LectorId))
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
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "GebruikerId", lector.GebruikerId);
            return View(lector);
        }

        // GET: Lectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lector == null)
            {
                return NotFound();
            }

            var lector = await _context.Lector
                .Include(l => l.Gebruiker)
                .FirstOrDefaultAsync(m => m.LectorId == id);
            if (lector == null)
            {
                return NotFound();
            }

            return View(lector);
        }

        // POST: Lectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lector == null)
            {
                return Problem("Entity set 'AppDbContext.Lector'  is null.");
            }
            var lector = await _context.Lector.FindAsync(id);
            if (lector != null)
            {
                _context.Lector.Remove(lector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LectorExists(int id)
        {
          return _context.Lector.Any(e => e.LectorId == id);
        }
    }
}
