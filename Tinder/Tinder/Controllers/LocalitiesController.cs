using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tinder.Data;
using Tinder.Models;

namespace Tinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalitiesController : ControllerBase
    {
        private readonly TinderContext _context;

        public LocalitiesController(TinderContext context)
        {
            _context = context;
        }

        // GET: api/Localities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locality>>> GetLocality()
        {
          if (_context.Locality == null)
          {
              return NotFound();
          }
            return await _context.Locality.ToListAsync();
        }

        // GET: api/Localities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Locality>> GetLocality(int id)
        {
          if (_context.Locality == null)
          {
              return NotFound();
          }
            var locality = await _context.Locality.FindAsync(id);

            if (locality == null)
            {
                return NotFound();
            }

            return locality;
        }

        // PUT: api/Localities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocality(int id, Locality locality)
        {
            if (id != locality.Id)
            {
                return BadRequest();
            }

            _context.Entry(locality).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Localities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Locality>> PostLocality(Locality locality)
        {
          if (_context.Locality == null)
          {
              return Problem("Entity set 'TinderContext.Locality'  is null.");
          }
            _context.Locality.Add(locality);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocality", new { id = locality.Id }, locality);
        }

        // DELETE: api/Localities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocality(int id)
        {
            if (_context.Locality == null)
            {
                return NotFound();
            }
            var locality = await _context.Locality.FindAsync(id);
            if (locality == null)
            {
                return NotFound();
            }

            _context.Locality.Remove(locality);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocalityExists(int id)
        {
            return (_context.Locality?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
