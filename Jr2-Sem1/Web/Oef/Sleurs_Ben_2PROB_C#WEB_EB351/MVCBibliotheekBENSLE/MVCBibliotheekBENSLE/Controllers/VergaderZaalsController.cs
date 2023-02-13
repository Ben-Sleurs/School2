using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBibliotheekBENSLE.Data;
using MVCBibliotheekBENSLE.Data.DefaultData;
using MVCBibliotheekBENSLE.Models.Data;

namespace MVCBibliotheekBENSLE.Controllers
{
    public class VergaderZaalsController : Controller
    {
        private readonly AppDbContext _context;

        public VergaderZaalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VergaderZaals
        public async Task<IActionResult> Index()
        {
              return View(await _context.VergaderZaal.ToListAsync());
        }

        // GET: VergaderZaals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VergaderZaal == null)
            {
                return NotFound();
            }

            var vergaderZaal = await _context.VergaderZaal
                .FirstOrDefaultAsync(m => m.VergaderZaalId == id);
            if (vergaderZaal == null)
            {
                return NotFound();
            }

            return View(vergaderZaal);
        }

        // GET: VergaderZaals/Create
        [Authorize(Roles = Roles.BibManager)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VergaderZaals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VergaderZaalId,Naam,AantalPersonen")] VergaderZaal vergaderZaal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vergaderZaal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vergaderZaal);
        }

        // GET: VergaderZaals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VergaderZaal == null)
            {
                return NotFound();
            }

            var vergaderZaal = await _context.VergaderZaal.FindAsync(id);
            if (vergaderZaal == null)
            {
                return NotFound();
            }
            return View(vergaderZaal);
        }

        // POST: VergaderZaals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("VergaderZaalId,Naam,AantalPersonen")] VergaderZaal vergaderZaal)
        {
            if (id != vergaderZaal.VergaderZaalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vergaderZaal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VergaderZaalExists(vergaderZaal.VergaderZaalId))
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
            return View(vergaderZaal);
        }

        // GET: VergaderZaals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VergaderZaal == null)
            {
                return NotFound();
            }

            var vergaderZaal = await _context.VergaderZaal
                .FirstOrDefaultAsync(m => m.VergaderZaalId == id);
            if (vergaderZaal == null)
            {
                return NotFound();
            }

            return View(vergaderZaal);
        }

        // POST: VergaderZaals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.VergaderZaal == null)
            {
                return Problem("Entity set 'AppDbContext.VergaderZaal'  is null.");
            }
            var vergaderZaal = await _context.VergaderZaal.FindAsync(id);
            if (vergaderZaal != null)
            {
                _context.VergaderZaal.Remove(vergaderZaal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VergaderZaalExists(int? id)
        {
          return _context.VergaderZaal.Any(e => e.VergaderZaalId == id);
        }
    }
}
