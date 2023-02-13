using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using azertyyyyyyy.Data;
using azertyyyyyyy.Models;

namespace azertyyyyyyy.Controllers
{
    public class EenModelsController : Controller
    {
        private readonly Database _context;

        public EenModelsController(Database context)
        {
            _context = context;
        }

        // GET: EenModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.EenModels.ToListAsync());
        }

        // GET: EenModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EenModels == null)
            {
                return NotFound();
            }

            var eenModel = await _context.EenModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eenModel == null)
            {
                return NotFound();
            }

            return View(eenModel);
        }

        // GET: EenModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EenModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam")] EenModel eenModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eenModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eenModel);
        }

        // GET: EenModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EenModels == null)
            {
                return NotFound();
            }

            var eenModel = await _context.EenModels.FindAsync(id);
            if (eenModel == null)
            {
                return NotFound();
            }
            return View(eenModel);
        }

        // POST: EenModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam")] EenModel eenModel)
        {
            if (id != eenModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eenModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EenModelExists(eenModel.Id))
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
            return View(eenModel);
        }

        // GET: EenModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EenModels == null)
            {
                return NotFound();
            }

            var eenModel = await _context.EenModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eenModel == null)
            {
                return NotFound();
            }

            return View(eenModel);
        }

        // POST: EenModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EenModels == null)
            {
                return Problem("Entity set 'Database.EenModels'  is null.");
            }
            var eenModel = await _context.EenModels.FindAsync(id);
            if (eenModel != null)
            {
                _context.EenModels.Remove(eenModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EenModelExists(int id)
        {
          return _context.EenModels.Any(e => e.Id == id);
        }
    }
}
