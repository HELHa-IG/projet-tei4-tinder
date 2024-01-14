using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tinder.Data;
using Tinder.Models;

namespace Tinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchLikesController : ControllerBase
    {
        private readonly TinderContext _context;

        public MatchLikesController(TinderContext context)
        {
            _context = context;
        }

        // GET: api/MatchLikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchLike>>> GetMatchLike()
        {
            if (_context.MatchLike == null)
            {
                return NotFound();
            }
            return await _context.MatchLike.ToListAsync();
        }

        // GET: api/MatchLikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchLike>> GetMatchLike(int id)
        {
            if (_context.MatchLike == null)
            {
                return NotFound();
            }
            var matchLike = await _context.MatchLike.FindAsync(id);

            if (matchLike == null)
            {
                return NotFound();
            }

            return matchLike;
        }
        // GET: api/MatchLikes
        [HttpGet("GetMatchLikeByUserIdAndScore/{id}")]
        public async Task<ActionResult<IEnumerable<MatchLike>>> GetMatchLikeByUserIdAndScore(int id)
        {
            if (_context.MatchLike == null)
            {
                return NotFound();
            }
            return await _context.MatchLike.Where(m => (m.IdUser01 == id || m.IdUser02 == id) && (m.ScoreUser01 >= 2 && m.ScoreUser02 >= 2)).ToListAsync();
        }

        // GET: api/MatchLikes/5
        [HttpGet("/GetMatchLikeByTwoUserId/{idUser01}/{idUser02}")]
        public async Task<ActionResult<MatchLike>> GetMatchLikeByTwoUserId(int idUser01, int idUser02)
        {
            if (_context.MatchLike == null)
            {
                return NotFound();
            }
            var matchLike = await _context.MatchLike.Where(m => (m.IdUser01 == idUser01 && m.IdUser02 == idUser02) || (m.IdUser01 == idUser02 && m.IdUser02 == idUser01)).FirstAsync();

            if (matchLike == null)
            {
                return NotFound();
            }

            return matchLike;
        }

        // PUT: api/MatchLikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatchLike(int id, MatchLike matchLike)
        {
            if (id != matchLike.Id)
            {
                return BadRequest();
            }

            _context.Entry(matchLike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchLikeExists(id))
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

        // POST: api/MatchLikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MatchLike>> PostMatchLike(MatchLike matchLike)
        {
            if (_context.MatchLike == null)
            {
                return Problem("Entity set 'TinderContext.MatchLike' is null.");
            }

            if (_context.Users == null)
            {
                return Problem("Entity set 'TinderContext.Users' is null.");
            }

            // Vérifiez si les IdUsers spécifiés existent
            var user1Exists = await _context.Users.AnyAsync(u => u.Id == matchLike.IdUser01);
            var user2Exists = await _context.Users.AnyAsync(u => u.Id == matchLike.IdUser02);

            if (!user1Exists || !user2Exists)
            {
                return BadRequest("One or both of the specified IdUsers do not exist.");
            }

            _context.MatchLike.Add(matchLike);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatchLike", new { id = matchLike.Id }, matchLike);
        }

        // DELETE: api/MatchLikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchLike(int id)
        {
            if (_context.MatchLike == null)
            {
                return NotFound();
            }
            var matchLike = await _context.MatchLike.FindAsync(id);
            if (matchLike == null)
            {
                return NotFound();
            }

            _context.MatchLike.Remove(matchLike);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchLikeExists(int id)
        {
            return (_context.MatchLike?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
