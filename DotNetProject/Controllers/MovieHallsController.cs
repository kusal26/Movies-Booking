using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetProject.Models;

namespace DotNetProject.Controllers
{
    public class MovieHallsController : Controller
    {
        private readonly DotNetProjectContext _context;

        public MovieHallsController(DotNetProjectContext context)
        {
            _context = context;
        }

        // GET: MovieHalls
        public async Task<IActionResult> Index()
        {
              return _context.MovieHall != null ? 
                          View(await _context.MovieHall.Include(x=>x.Timing).ToListAsync()) :
                          Problem("Entity set 'DotNetProjectContext.MovieHall'  is null.");
        }

        // GET: MovieHalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovieHall == null)
            {
                return NotFound();
            }

            var movieHall = await _context.MovieHall
                .FirstOrDefaultAsync(m => m.HallId == id);
            if (movieHall == null)
            {
                return NotFound();
            }

            return View(movieHall);
        }

        // GET: MovieHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HallId,HallName,HallLocation,TotalSeat")] MovieHall movieHall)
        {
           
                _context.Add(movieHall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: MovieHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovieHall == null)
            {
                return NotFound();
            }

            var movieHall = await _context.MovieHall.FindAsync(id);
            if (movieHall == null)
            {
                return NotFound();
            }
            return View(movieHall);
        }

        // POST: MovieHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HallId,HallName,HallLocation,TotalSeat")] MovieHall movieHall)
        {
            if (id != movieHall.HallId)
            {
                return NotFound();
            }

          
                try
                {
                    _context.Update(movieHall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieHallExists(movieHall.HallId))
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

        // GET: MovieHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovieHall == null)
            {
                return NotFound();
            }

            var movieHall = await _context.MovieHall
                .FirstOrDefaultAsync(m => m.HallId == id);
            if (movieHall == null)
            {
                return NotFound();
            }

            return View(movieHall);
        }

        // POST: MovieHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieHall == null)
            {
                return Problem("Entity set 'DotNetProjectContext.MovieHall'  is null.");
            }
            var movieHall = await _context.MovieHall.FindAsync(id);
            if (movieHall != null)
            {
                _context.MovieHall.Remove(movieHall);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieHallExists(int id)
        {
          return (_context.MovieHall?.Any(e => e.HallId == id)).GetValueOrDefault();
        }
    }
}
