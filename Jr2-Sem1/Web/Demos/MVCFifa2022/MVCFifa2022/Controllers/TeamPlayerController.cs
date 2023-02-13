using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCFifa2022.Data;
using MVCFifa2022.Models;

namespace MVCFifa2022.Controllers
{
    public class TeamPlayerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamPlayerController(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // GET: TeamPlayer
        public async Task<IActionResult> Index()
        {
              return View(await _context.TeamPlayer.ToListAsync());
        }

        // GET: TeamPlayer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeamPlayer == null)
            {
                return NotFound();
            }

            var teamPlayer = await _context.TeamPlayer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamPlayer == null)
            {
                return NotFound();
            }

            return View(teamPlayer);
        }

        // GET: TeamPlayer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TeamPlayer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamId,PlayerId,StartDate,EndDate")] TeamPlayer teamPlayer)
        {
            if (ModelState.IsValid)
            {
                if (_context.Players.Where(x=>x.PlayerId == teamPlayer.PlayerId).Count()==0)
                {
                    ModelState.AddModelError("Error", "Foutieve foreign key met de players tabel");
                    return View(teamPlayer);
                }
                if (_context.Teams.Where(x => x.TeamId == teamPlayer.TeamId).Count() == 0)
                {
                    ModelState.AddModelError("Error", "foutieve foreign key met de teams tabel");
                    return View(teamPlayer);
                }
                _context.Add(teamPlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teamPlayer);
        }

        // GET: TeamPlayer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeamPlayer == null)
            {
                return NotFound();
            }

            var teamPlayer = await _context.TeamPlayer.FindAsync(id);
            if (teamPlayer == null)
            {
                return NotFound();
            }
            return View(teamPlayer);
        }

        // POST: TeamPlayer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamId,PlayerId,StartDate,EndDate")] TeamPlayer teamPlayer)
        {
            if (id != teamPlayer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamPlayerExists(teamPlayer.Id))
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
            return View(teamPlayer);
        }

        // GET: TeamPlayer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeamPlayer == null)
            {
                return NotFound();
            }

            var teamPlayer = await _context.TeamPlayer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamPlayer == null)
            {
                return NotFound();
            }

            return View(teamPlayer);
        }

        // POST: TeamPlayer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeamPlayer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TeamPlayer'  is null.");
            }
            var teamPlayer = await _context.TeamPlayer.FindAsync(id);
            if (teamPlayer != null)
            {
                _context.TeamPlayer.Remove(teamPlayer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamPlayerExists(int id)
        {
          return _context.TeamPlayer.Any(e => e.Id == id);
        }
    }
}
