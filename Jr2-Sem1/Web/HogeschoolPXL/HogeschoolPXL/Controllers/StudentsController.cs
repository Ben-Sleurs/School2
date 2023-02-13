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
using HogeschoolPXL.Models.ViewModels;

namespace HogeschoolPXL.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class StudentsController : Controller
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Student
                .Include(s => s.Gebruiker);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Gebruiker)
                .Include(s => s.Inschrijvingen)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }
            var studentDetails = new StudentDetailsViewModel();
            studentDetails.Student = student;

            var inschrijvingen = _context.Inschrijving
                .Where(x => x.StudentId == student.StudentId)
                .Include(i => i.VakLector)
                .ThenInclude(i => i.Vak)
                .ThenInclude(i => i.Handboek)
                .Include(i => i.VakLector.Lector)
                .ThenInclude(i => i.Gebruiker)
                .Include(i => i.AcademieJaar)
                .ToList();

            studentDetails.Inschrijvingen = inschrijvingen;
            return View(studentDetails);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var GebruikersExclusiefLectorenEnStudenten = _context.Gebruiker.Where(x => x.Lector == null && x.Student == null);
            ViewData["GebruikerId"] = GebruikersExclusiefLectorenEnStudenten.Select(x => new SelectListItem()
            {
                Value = x.GebruikerId.ToString(),
                Text = $"{x.Naam} {x.Voornaam}"
            });
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,GebruikerId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "GebruikerId", student.GebruikerId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var GebruikersExclusiefLectorenEnStudenten = _context.Gebruiker.Where(x => x.Lector == null && x.Student == null);
            ViewData["GebruikerId"] = GebruikersExclusiefLectorenEnStudenten.Select(x => new SelectListItem()
            {
                Value = x.GebruikerId.ToString(),
                Text = $"{x.Naam} {x.Voornaam}"
            });
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,GebruikerId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            ViewData["GebruikerId"] = new SelectList(_context.Gebruiker, "GebruikerId", "GebruikerId", student.GebruikerId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.Gebruiker)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'AppDbContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
