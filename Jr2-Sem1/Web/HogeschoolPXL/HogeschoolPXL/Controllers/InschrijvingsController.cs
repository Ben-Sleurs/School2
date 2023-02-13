using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Data;
using HogeschoolPXL.Models.Data;
using Microsoft.AspNetCore.Authorization;
using HogeschoolPXL.Data.DefaultData;

namespace HogeschoolPXL.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class InschrijvingsController : Controller
    {
        private readonly AppDbContext _context;

        public InschrijvingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Inschrijvings
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Inschrijving
                .Include(i => i.AcademieJaar)
                .Include(i => i.Student)
                .Include(i => i.VakLector)
                .Include(i => i.Student.Gebruiker)
                .Include(i => i.VakLector.Vak)
                .Include(i => i.VakLector.Lector)
                .Include(i => i.VakLector.Lector.Gebruiker);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Inschrijvings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inschrijving == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijving
                .Include(i => i.AcademieJaar)
                .Include(i => i.Student)
                .Include(i => i.VakLector)
                .Include(i => i.Student.Gebruiker)
                .Include(i => i.VakLector.Vak)
                .Include(i => i.VakLector.Lector)
                .Include(i => i.VakLector.Lector.Gebruiker)
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        // GET: Inschrijvings/Create
        public IActionResult Create()
        {
            ViewData["AcademieJaarId"] = _context.AcademieJaar.Select(x => new SelectListItem()
            {
                Value = x.AcademieJaarId.ToString(),
                Text = $"{x.StartDatum.Year} - {x.StartDatum.Year + 1}"
            });
            ViewData["StudentId"] = _context.Student.Select(x => new SelectListItem()
            {
                Value = x.StudentId.ToString(),
                Text = $"{x.Gebruiker.Naam} {x.Gebruiker.Voornaam}"
            });
            ViewData["VakLectorId"] = _context.VakLector.Select(x => new SelectListItem()
            {
                Value = x.VakLectorId.ToString(),
                Text = $"{x.Vak.VakNaam} - {x.Lector.Gebruiker.Naam} {x.Lector.Gebruiker.Voornaam}"
            });
            return View();
        }

        // POST: Inschrijvings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InschrijvingId,StudentId,VakLectorId,AcademieJaarId")] Inschrijving inschrijving)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inschrijving);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademieJaarId"] = _context.AcademieJaar.Select(x => new SelectListItem()
            {
                Value = x.AcademieJaarId.ToString(),
                Text = $"{x.StartDatum.Year} - {x.StartDatum.Year + 1}"
            });
            ViewData["StudentId"] = _context.Student.Select(x => new SelectListItem()
            {
                Value = x.StudentId.ToString(),
                Text = $"{x.Gebruiker.Naam} {x.Gebruiker.Voornaam}"
            });
            ViewData["VakLectorId"] = _context.VakLector.Select(x => new SelectListItem()
            {
                Value = x.VakLectorId.ToString(),
                Text = $"{x.Vak.VakNaam} - {x.Lector.Gebruiker.Naam} {x.Lector.Gebruiker.Voornaam}"
            });
            return View(inschrijving);
        }

        // GET: Inschrijvings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inschrijving == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijving.FindAsync(id);
            if (inschrijving == null)
            {
                return NotFound();
            }
            ViewData["AcademieJaarId"] = _context.AcademieJaar.Select(x => new SelectListItem() { 
                Value = x.AcademieJaarId.ToString(), 
                Text = $"{x.StartDatum.Year} - {x.StartDatum.Year +1}" });
            ViewData["StudentId"] = _context.Student.Select(x => new SelectListItem()
            {
                Value = x.StudentId.ToString(),
                Text = $"{x.Gebruiker.Naam} {x.Gebruiker.Voornaam}"
            });
            ViewData["VakLectorId"] = _context.VakLector.Select(x => new SelectListItem()
            {
                Value = x.VakLectorId.ToString(),
                Text = $"{x.Vak.VakNaam} - {x.Lector.Gebruiker.Naam} {x.Lector.Gebruiker.Voornaam}"
            });
            return View(inschrijving);
        }

        // POST: Inschrijvings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InschrijvingId,StudentId,VakLectorId,AcademieJaarId")] Inschrijving inschrijving)
        {
            if (id != inschrijving.InschrijvingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inschrijving);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InschrijvingExists(inschrijving.InschrijvingId))
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
            ViewData["AcademieJaarId"] = _context.AcademieJaar.Select(x => new SelectListItem()
            {
                Value = x.AcademieJaarId.ToString(),
                Text = $"{x.StartDatum.Year} - {x.StartDatum.Year + 1}"
            });
            ViewData["StudentId"] = _context.Student.Select(x => new SelectListItem()
            {
                Value = x.StudentId.ToString(),
                Text = $"{x.Gebruiker.Naam} {x.Gebruiker.Voornaam}"
            });
            ViewData["VakLectorId"] = _context.VakLector.Select(x => new SelectListItem()
            {
                Value = x.VakLectorId.ToString(),
                Text = $"{x.Vak.VakNaam} - {x.Lector.Gebruiker.Naam} {x.Lector.Gebruiker.Voornaam}"
            });
            return View(inschrijving);
        }

        // GET: Inschrijvings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inschrijving == null)
            {
                return NotFound();
            }

            var inschrijving = await _context.Inschrijving
                .Include(i => i.AcademieJaar)
                .Include(i => i.Student)
                .Include(i => i.VakLector)
                .FirstOrDefaultAsync(m => m.InschrijvingId == id);
            if (inschrijving == null)
            {
                return NotFound();
            }

            return View(inschrijving);
        }

        // POST: Inschrijvings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inschrijving == null)
            {
                return Problem("Entity set 'AppDbContext.Inschrijving'  is null.");
            }
            var inschrijving = await _context.Inschrijving.FindAsync(id);
            if (inschrijving != null)
            {
                _context.Inschrijving.Remove(inschrijving);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InschrijvingExists(int id)
        {
          return _context.Inschrijving.Any(e => e.InschrijvingId == id);
        }
    }
}
