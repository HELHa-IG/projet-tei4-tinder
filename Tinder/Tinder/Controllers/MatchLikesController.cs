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
              return Problem("Entity set 'TinderContext.MatchLike'  is null.");
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

        //Check if two person match <3
        [HttpGet("GetMatches/{id}")]
        public ActionResult<IEnumerable<MatchLike>> GetMatches(string id)
        {
            if (_context.MatchLike == null)
            {
                return NotFound();
            }
            var allMatches = _context.MatchLike.ToList();

            // Nous allons récupérer uniquement les utilisateurs qui se sont match,
            // et dans le front, nous n'aurons qu'à vérifier si les scores sont vides pour afficher le questionnaire
            var filteredMatches = allMatches.Where(match =>
                (match.IdUser01.CompareTo(id) < 0 || match.IdUser02.CompareTo(id) < 0) ||
                (match.User01Like && match.User02Like)
            ).ToList();

            //return match list
            return Ok(filteredMatches);
        }
    }
}
