using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PXLFilmzaal.Data;
using PXLFilmzaal.Helpers;
using PXLFilmzaal.Models.Data;

namespace PXLFilmzaal.Controllers
{
    public class FilmImagesController : Controller
    {
        private readonly AppDbContext _context;

        public FilmImagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FilmImages
        public async Task<IActionResult> Index()
        {
              return View(await _context.FilmImages.ToListAsync());
        }

        // GET: FilmImages/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.FilmImages == null)
            {
                return NotFound();
            }

            var filmImage = await _context.FilmImages
                .FirstOrDefaultAsync(m => m.FilmImageId == id);
            if (filmImage == null)
            {
                return NotFound();
            }
            if (filmImage.FilmImageData != null)
            {
                ViewBag.ImageDataUrl = FileHelper.CreateBase64StringFromByteArray(filmImage.FilmImageData);
            }

            return View(filmImage);
        }

        // GET: FilmImages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FilmImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmImageId,FilmImageData,FilmImageName")] FilmImage filmImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filmImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filmImage);
        }

        // GET: FilmImages/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.FilmImages == null)
            {
                return NotFound();
            }

            var filmImage = await _context.FilmImages.FindAsync(id);
            if (filmImage == null)
            {
                return NotFound();
            }
            return View(filmImage);
        }

        // POST: FilmImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FilmImageId,FilmImageData,FilmImageName")] FilmImage filmImage)
        {
            if (id != filmImage.FilmImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(filmImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmImageExists(filmImage.FilmImageId))
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
            return View(filmImage);
        }

        // GET: FilmImages/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.FilmImages == null)
            {
                return NotFound();
            }

            var filmImage = await _context.FilmImages
                .FirstOrDefaultAsync(m => m.FilmImageId == id);
            if (filmImage == null)
            {
                return NotFound();
            }

            return View(filmImage);
        }

        // POST: FilmImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.FilmImages == null)
            {
                return Problem("Entity set 'AppDbContext.FilmImages'  is null.");
            }
            var filmImage = await _context.FilmImages.FindAsync(id);
            if (filmImage != null)
            {
                _context.FilmImages.Remove(filmImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmImageExists(string id)
        {
          return _context.FilmImages.Any(e => e.FilmImageId == id);
        }
    }
}
