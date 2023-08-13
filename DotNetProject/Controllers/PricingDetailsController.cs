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
    public class PricingDetailsController : Controller
    {
        private readonly DotNetProjectContext _context;

        public PricingDetailsController(DotNetProjectContext context)
        {
            _context = context;
        }

        // GET: PricingDetails
        public async Task<IActionResult> Index()
        {
              return _context.PricingDetails != null ? 
                          View(await _context.PricingDetails.Include(x=>x.Movies).ToListAsync()) :
                          Problem("Entity set 'DotNetProjectContext.PricingDetails'  is null.");
        }

        // GET: PricingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PricingDetails == null)
            {
                return NotFound();
            }

            var pricingDetails = await _context.PricingDetails
                .FirstOrDefaultAsync(m => m.PriceId == id);
            if (pricingDetails == null)
            {
                return NotFound();
            }

            return View(pricingDetails);
        }

        // GET: PricingDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PricingDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriceId,Price")] PricingDetails pricingDetails)
        {
           
                _context.Add(pricingDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           // return View(pricingDetails);
        }

        // GET: PricingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PricingDetails == null)
            {
                return NotFound();
            }

            var pricingDetails = await _context.PricingDetails.FindAsync(id);
            if (pricingDetails == null)
            {
                return NotFound();
            }
            return View(pricingDetails);
        }

        // POST: PricingDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PriceId,Price")] PricingDetails pricingDetails)
        {
            if (id != pricingDetails.PriceId)
            {
                return NotFound();
            }

          
                try
                {
                    _context.Update(pricingDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PricingDetailsExists(pricingDetails.PriceId))
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
         
        

        // GET: PricingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PricingDetails == null)
            {
                return NotFound();
            }

            var pricingDetails = await _context.PricingDetails
                .FirstOrDefaultAsync(m => m.PriceId == id);
            if (pricingDetails == null)
            {
                return NotFound();
            }

            return View(pricingDetails);
        }

        // POST: PricingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PricingDetails == null)
            {
                return Problem("Entity set 'DotNetProjectContext.PricingDetails'  is null.");
            }
            var pricingDetails = await _context.PricingDetails.FindAsync(id);
            if (pricingDetails != null)
            {
                _context.PricingDetails.Remove(pricingDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PricingDetailsExists(int id)
        {
          return (_context.PricingDetails?.Any(e => e.PriceId == id)).GetValueOrDefault();
        }
    }
}
