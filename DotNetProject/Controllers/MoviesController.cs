using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetProject.Models;
using Microsoft.AspNetCore.Hosting;

namespace DotNetProject.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DotNetProjectContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public MoviesController(DotNetProjectContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var dotNetProjectContext = _context.Movies.Include(m => m.PricingDetails).Include(x=>x.showTimings);
            return View(await dotNetProjectContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .Include(m => m.PricingDetails)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["PriceId"] = new SelectList(_context.Set<PricingDetails>(), "PriceId", "Price");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Movies movies)
        {
            if (movies.ImageFile != null)
            {

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + movies.ImageFile.FileName;
                string folder = "images/";


                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                string filePath = Path.Combine(serverfolder, uniqueFileName);
                await movies.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

                movies.ImageUrl = "images/" + uniqueFileName;
            }
            _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            ViewData["PriceId"] = new SelectList(_context.Set<PricingDetails>(), "PriceId", "Price", movies.PriceId);
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Name,Description,Cast,Genre,Duration,PriceId,ReleaseDate,ImageUrl")] Movies movies)
        {
            if (id != movies.MovieId)
            {
                return NotFound();
            }

            
                try
                {
                if (movies.ImageFile != null)
                {

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + movies.ImageFile.FileName;
                    string folder = "images/";


                    string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    string filePath = Path.Combine(serverfolder, uniqueFileName);
                    await movies.ImageFile.CopyToAsync(new FileStream(filePath, FileMode.Create));

                    movies.ImageUrl = "images/" + uniqueFileName;
                }
                _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.MovieId))
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

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .Include(m => m.PricingDetails)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'DotNetProjectContext.Movies'  is null.");
            }
            var movies = await _context.Movies.FindAsync(id);
            if (movies != null)
            {
                _context.Movies.Remove(movies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
          return (_context.Movies?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
