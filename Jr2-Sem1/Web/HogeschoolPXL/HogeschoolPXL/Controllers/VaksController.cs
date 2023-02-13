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

namespace HogeschoolPXL.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class VaksController : Controller
    {
        private readonly AppDbContext _context;

        public VaksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Vaks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Vak.Include(v => v.Handboek);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Vaks/Create
        public IActionResult Create()
        {
            ViewData["HandboekId"] = _context.Handboek.Select(x => new SelectListItem()
            {
                Value = x.HandboekId.ToString(),
                Text = x.Titel
            });
            return View();
        }

        // POST: Vaks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VakId,VakNaam,Studiepunten,HandboekId")] Vak vak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HandboekId"] = _context.Handboek.Select(x => new SelectListItem()
            {
                Value = x.HandboekId.ToString(),
                Text = x.Titel
            });
            return View(vak);
        }

        // GET: Vaks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vak == null)
            {
                return NotFound();
            }

            var vak = await _context.Vak.FindAsync(id);
            if (vak == null)
            {
                return NotFound();
            }
            ViewData["HandboekId"] = _context.Handboek.Select(x => new SelectListItem()
            {
                Value = x.HandboekId.ToString(),
                Text = x.Titel
            });
            return View(vak);
        }

        // POST: Vaks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VakId,VakNaam,Studiepunten,HandboekId")] Vak vak)
        {
            if (id != vak.VakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakExists(vak.VakId))
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
            ViewData["HandboekId"] = _context.Handboek.Select(x => new SelectListItem()
            {
                Value = x.HandboekId.ToString(),
                Text = x.Titel
            });
            return View(vak);
        }

        // GET: Vaks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vak == null)
            {
                return NotFound();
            }

            var vak = await _context.Vak
                .Include(v => v.Handboek)
                .FirstOrDefaultAsync(m => m.VakId == id);
            if (vak == null)
            {
                return NotFound();
            }

            return View(vak);
        }

        // POST: Vaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vak == null)
            {
                return Problem("Entity set 'AppDbContext.Vak'  is null.");
            }
            var vak = await _context.Vak.FindAsync(id);
            if (vak != null)
            {
                _context.Vak.Remove(vak);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakExists(int id)
        {
          return _context.Vak.Any(e => e.VakId == id);
        }
    }
}
