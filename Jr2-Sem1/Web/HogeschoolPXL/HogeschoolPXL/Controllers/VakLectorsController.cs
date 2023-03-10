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
    public class VakLectorsController : Controller
    {
        private readonly AppDbContext _context;

        public VakLectorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VakLectors
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.VakLector.Include(v => v.Lector).Include(v => v.Vak);
            return View(await appDbContext.ToListAsync());
        }

        // GET: VakLectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VakLector == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLector
                .Include(v => v.Lector)
                .Include(v => v.Vak)
                .FirstOrDefaultAsync(m => m.VakLectorId == id);
            if (vakLector == null)
            {
                return NotFound();
            }

            return View(vakLector);
        }

        // GET: VakLectors/Create
        public IActionResult Create()
        {
            ViewData["LectorId"] = new SelectList(_context.Lector, "LectorId", "LectorId");
            ViewData["VakId"] = new SelectList(_context.Vak, "VakId", "VakId");
            return View();
        }

        // POST: VakLectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VakLectorId,LectorId,VakId")] VakLector vakLector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vakLector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LectorId"] = new SelectList(_context.Lector, "LectorId", "LectorId", vakLector.LectorId);
            ViewData["VakId"] = new SelectList(_context.Vak, "VakId", "VakId", vakLector.VakId);
            return View(vakLector);
        }

        // GET: VakLectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VakLector == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLector.FindAsync(id);
            if (vakLector == null)
            {
                return NotFound();
            }
            ViewData["LectorId"] = new SelectList(_context.Lector, "LectorId", "LectorId", vakLector.LectorId);
            ViewData["VakId"] = new SelectList(_context.Vak, "VakId", "VakId", vakLector.VakId);
            return View(vakLector);
        }

        // POST: VakLectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VakLectorId,LectorId,VakId")] VakLector vakLector)
        {
            if (id != vakLector.VakLectorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vakLector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VakLectorExists(vakLector.VakLectorId))
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
            ViewData["LectorId"] = new SelectList(_context.Lector, "LectorId", "LectorId", vakLector.LectorId);
            ViewData["VakId"] = new SelectList(_context.Vak, "VakId", "VakId", vakLector.VakId);
            return View(vakLector);
        }

        // GET: VakLectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VakLector == null)
            {
                return NotFound();
            }

            var vakLector = await _context.VakLector
                .Include(v => v.Lector)
                .Include(v => v.Vak)
                .FirstOrDefaultAsync(m => m.VakLectorId == id);
            if (vakLector == null)
            {
                return NotFound();
            }

            return View(vakLector);
        }

        // POST: VakLectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VakLector == null)
            {
                return Problem("Entity set 'AppDbContext.VakLector'  is null.");
            }
            var vakLector = await _context.VakLector.FindAsync(id);
            if (vakLector != null)
            {
                _context.VakLector.Remove(vakLector);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VakLectorExists(int id)
        {
          return _context.VakLector.Any(e => e.VakLectorId == id);
        }
    }
}
