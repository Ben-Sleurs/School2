﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTagHelper.Data;
using MVCTagHelper.Models;
using MVCTagHelper.ViewModels;

namespace MVCTagHelper.Controllers
{
    [Authorize]
    public class MedewerkersController : Controller
    {
        private readonly TagHelperDbContext _context;

        public MedewerkersController(TagHelperDbContext context)
        {
            _context = context;
        }

        // GET: Medewerkers
        public async Task<IActionResult> Index()
        {
            var tagHelperDbContext = _context.Medewerker.Include(m => m.Afdeling);
            return View(await tagHelperDbContext.ToListAsync());
        }

        // GET: Medewerkers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medewerker == null)
            {
                return NotFound();
            }

            var medewerker = await _context.Medewerker
                .Include(m => m.Afdeling)
                .FirstOrDefaultAsync(m => m.MedewerkerId == id);
            if (medewerker == null)
            {
                return NotFound();
            }
            var medewerkerCard = new MedewerkerCard(_context, medewerker);
            return View(medewerkerCard);
        }

        // GET: Medewerkers/Create
        public IActionResult Create()
        {
            ViewData["AfdelingId"] = new SelectList(_context.Afdelingen, "AfdelingId", "AfdelingId");
            return View();
        }

        // POST: Medewerkers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedewerkerId,AfdelingId,Naam,VoorNaam,Email")] Medewerker medewerker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medewerker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AfdelingId"] = new SelectList(_context.Afdelingen, "AfdelingId", "AfdelingId", medewerker.AfdelingId);
            return View(medewerker);
        }

        // GET: Medewerkers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medewerker == null)
            {
                return NotFound();
            }

            var medewerker = await _context.Medewerker.FindAsync(id);
            if (medewerker == null)
            {
                return NotFound();
            }
            ViewData["AfdelingId"] = new SelectList(_context.Afdelingen, "AfdelingId", "AfdelingId", medewerker.AfdelingId);
            return View(medewerker);
        }

        // POST: Medewerkers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedewerkerId,AfdelingId,Naam,VoorNaam,Email")] Medewerker medewerker)
        {
            if (id != medewerker.MedewerkerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medewerker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedewerkerExists(medewerker.MedewerkerId))
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
            ViewData["AfdelingId"] = new SelectList(_context.Afdelingen, "AfdelingId", "AfdelingId", medewerker.AfdelingId);
            return View(medewerker);
        }

        // GET: Medewerkers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medewerker == null)
            {
                return NotFound();
            }

            var medewerker = await _context.Medewerker
                .Include(m => m.Afdeling)
                .FirstOrDefaultAsync(m => m.MedewerkerId == id);
            if (medewerker == null)
            {
                return NotFound();
            }

            return View(medewerker);
        }

        // POST: Medewerkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medewerker == null)
            {
                return Problem("Entity set 'TagHelperDbContext.Medewerker'  is null.");
            }
            var medewerker = await _context.Medewerker.FindAsync(id);
            if (medewerker != null)
            {
                _context.Medewerker.Remove(medewerker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedewerkerExists(int id)
        {
          return _context.Medewerker.Any(e => e.MedewerkerId == id);
        }
    }
}
