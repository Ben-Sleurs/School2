using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGroentenEnFruit.Data;
using MVCGroentenEnFruit.Data.DefaultData;
using MVCGroentenEnFruit.Models.Data;

namespace MVCGroentenEnFruit.Controllers
{
    [Authorize]
    public class VerkoopOrdersController : Controller
    {
        private readonly AppDbContext _context;

        public VerkoopOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VerkoopOrders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.VerkoopOrders.Include(v => v.Artikel).Include(v => v.IdentityUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: VerkoopOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VerkoopOrders == null)
            {
                return NotFound();
            }

            var verkoopOrder = await _context.VerkoopOrders
                .Include(v => v.Artikel)
                .Include(v => v.IdentityUser)
                .FirstOrDefaultAsync(m => m.VerkoopOrderId == id);
            if (verkoopOrder == null)
            {
                return NotFound();
            }

            return View(verkoopOrder);
        }

        // GET: VerkoopOrders/Create
        [Authorize(Roles = Roles.Verkoper)]
        public IActionResult Create()
        {
            ViewData["ArtikelId"] = new SelectList(_context.Artikels, "ArtikelId", "Naam");
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: VerkoopOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VerkoopOrderId,ArtikelId,Hoeveelheid,IdentityUserId")] VerkoopOrder verkoopOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verkoopOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtikelId"] = new SelectList(_context.Artikels, "ArtikelId", "ArtikelId", verkoopOrder.ArtikelId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", verkoopOrder.IdentityUserId);
            return View(verkoopOrder);
        }

        // GET: VerkoopOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VerkoopOrders == null)
            {
                return NotFound();
            }

            var verkoopOrder = await _context.VerkoopOrders.FindAsync(id);
            if (verkoopOrder == null)
            {
                return NotFound();
            }
            ViewData["ArtikelId"] = new SelectList(_context.Artikels, "ArtikelId", "ArtikelId", verkoopOrder.ArtikelId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", verkoopOrder.IdentityUserId);
            return View(verkoopOrder);
        }

        // POST: VerkoopOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VerkoopOrderId,ArtikelId,Hoeveelheid,IdentityUserId")] VerkoopOrder verkoopOrder)
        {
            if (id != verkoopOrder.VerkoopOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verkoopOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerkoopOrderExists(verkoopOrder.VerkoopOrderId))
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
            ViewData["ArtikelId"] = new SelectList(_context.Artikels, "ArtikelId", "ArtikelId", verkoopOrder.ArtikelId);
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", verkoopOrder.IdentityUserId);
            return View(verkoopOrder);
        }

        // GET: VerkoopOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VerkoopOrders == null)
            {
                return NotFound();
            }

            var verkoopOrder = await _context.VerkoopOrders
                .Include(v => v.Artikel)
                .Include(v => v.IdentityUser)
                .FirstOrDefaultAsync(m => m.VerkoopOrderId == id);
            if (verkoopOrder == null)
            {
                return NotFound();
            }

            return View(verkoopOrder);
        }

        // POST: VerkoopOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VerkoopOrders == null)
            {
                return Problem("Entity set 'AppDbContext.VerkoopOrders'  is null.");
            }
            var verkoopOrder = await _context.VerkoopOrders.FindAsync(id);
            if (verkoopOrder != null)
            {
                _context.VerkoopOrders.Remove(verkoopOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VerkoopOrderExists(int id)
        {
          return _context.VerkoopOrders.Any(e => e.VerkoopOrderId == id);
        }
    }
}
