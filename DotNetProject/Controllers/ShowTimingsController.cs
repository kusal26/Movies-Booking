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
    public class ShowTimingsController : Controller
    {
        private readonly DotNetProjectContext _context;

        public ShowTimingsController(DotNetProjectContext context)
        {
            _context = context;
        }

        // GET: ShowTimings
        public async Task<IActionResult> Index()
        {
              return _context.ShowTiming != null ? 
                          View(await _context.ShowTiming.Include(x=>x.Movies).Include(x=>x.MovieHall).ToListAsync()) :
                          Problem("Entity set 'DotNetProjectContext.ShowTiming'  is null.");
        }

        // GET: ShowTimings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShowTiming == null)
            {
                return NotFound();
            }

            var showTiming = await _context.ShowTiming.Include(x => x.Movies).Include(x => x.MovieHall)
                .FirstOrDefaultAsync(m => m.TimingId == id);
            if (showTiming == null)
            {
                return NotFound();
            }

            return View(showTiming);
        }

        // GET: ShowTimings/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Set<Movies>(), "MovieId", "Name");
            ViewData["HallId"] = new SelectList(_context.Set<MovieHall>(), "HallId", "HallName");


            return View();
        }

        // POST: ShowTimings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShowTiming showTiming)
        {
            var updateAvailableseat = _context.MovieHall.FirstOrDefault(x=>x.HallId== showTiming.HallId);

            showTiming.available_seats = updateAvailableseat?.TotalSeat;
                _context.Add(showTiming);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

        }

        // GET: ShowTimings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShowTiming == null)
            {
                return NotFound();
            }

            var showTiming = await _context.ShowTiming.FindAsync(id);
            if (showTiming == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Set<Movies>(), "MovieId", "Name");
            ViewData["HallId"] = new SelectList(_context.Set<MovieHall>(), "HallId", "HallName");
            return View(showTiming);
        }

        // POST: ShowTimings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimingId,MovieId,HallId,show_datetime,available_seats")] ShowTiming showTiming)
        {
            if (id != showTiming.TimingId)
            {
                return NotFound();
            }

            var updateAvailableticket = _context.MovieHall.FirstOrDefault(x => x.HallId == showTiming.HallId);
            if (showTiming.available_seats <= updateAvailableticket?.TotalSeat)
            {
                try
                {


                    _context.Update(showTiming);
                    await _context.SaveChangesAsync();

                }


                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowTimingExists(showTiming.TimingId))
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
            else
            {
                // Add a model error to show a warning message on the view
                ModelState.AddModelError("available_seats", "Available seats cannot exceed total seats in the movie hall");

                // Provide necessary ViewData for your dropdowns or other fields
                ViewData["MovieId"] = new SelectList(_context.Set<Movies>(), "MovieId", "Name");
                ViewData["HallId"] = new SelectList(_context.Set<MovieHall>(), "HallId", "HallName");

               
                return View(showTiming);
            }

           
        }

        // GET: ShowTimings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShowTiming == null)
            {
                return NotFound();
            }

            var showTiming = await _context.ShowTiming
                .FirstOrDefaultAsync(m => m.TimingId == id);
            if (showTiming == null)
            {
                return NotFound();
            }

            return View(showTiming);
        }

        // POST: ShowTimings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShowTiming == null)
            {
                return Problem("Entity set 'DotNetProjectContext.ShowTiming'  is null.");
            }
            var showTiming = await _context.ShowTiming.FindAsync(id);
            if (showTiming != null)
            {
                _context.ShowTiming.Remove(showTiming);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowTimingExists(int id)
        {
          return (_context.ShowTiming?.Any(e => e.TimingId == id)).GetValueOrDefault();
        }
    }
}
