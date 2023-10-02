using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tinder.Data;
using Tinder.Models;

namespace Tinder.Controllers
{
    public class LocalitiesController : Controller
    {
        private readonly TinderContext _context;

        public LocalitiesController(TinderContext context)
        {
            _context = context;
        }

        // GET: Localities
        public async Task<IActionResult> Index()
        {
              return _context.Locality != null ? 
                          View(await _context.Locality.ToListAsync()) :
                          Problem("Entity set 'TinderContext.Locality'  is null.");
        }

        // GET: Localities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Locality == null)
            {
                return NotFound();
            }

            var locality = await _context.Locality
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locality == null)
            {
                return NotFound();
            }

            return View(locality);
        }

        // GET: Localities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Localities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ville,Pays,Longitude,Latitude")] Locality locality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(locality);
        }

        // GET: Localities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Locality == null)
            {
                return NotFound();
            }

            var locality = await _context.Locality.FindAsync(id);
            if (locality == null)
            {
                return NotFound();
            }
            return View(locality);
        }

        // POST: Localities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ville,Pays,Longitude,Latitude")] Locality locality)
        {
            if (id != locality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalityExists(locality.Id))
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
            return View(locality);
        }

        // GET: Localities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Locality == null)
            {
                return NotFound();
            }

            var locality = await _context.Locality
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locality == null)
            {
                return NotFound();
            }

            return View(locality);
        }

        // POST: Localities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Locality == null)
            {
                return Problem("Entity set 'TinderContext.Locality'  is null.");
            }
            var locality = await _context.Locality.FindAsync(id);
            if (locality != null)
            {
                _context.Locality.Remove(locality);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocalityExists(int id)
        {
          return (_context.Locality?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
