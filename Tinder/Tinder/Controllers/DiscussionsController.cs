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
    public class DiscussionsController : ControllerBase
    {
        private readonly TinderContext _context;

        public DiscussionsController(TinderContext context)
        {
            _context = context;
        }

        // GET: api/Discussions
        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<IEnumerable<Discussion>>> GetDiscussion()
        {
          if (_context.Discussion == null)
          {
              return NotFound();
          }
            return await _context.Discussion.ToListAsync();
        }

        // GET: api/Discussions/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Discussion>> GetDiscussion(string id)
        {
          if (_context.Discussion == null)
          {
              return NotFound();
          }
            var discussion = await _context.Discussion.FindAsync(id);

            if (discussion == null)
            {
                return NotFound();
            }

            return discussion;
        }

        // PUT: api/Discussions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutDiscussion(string id, Discussion discussion)
        {
            if (id != discussion.Id)
            {
                return BadRequest();
            }

            _context.Entry(discussion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscussionExists(id))
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

        // POST: api/Discussions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Discussion>> PostDiscussion(Discussion discussion)
        {
          if (_context.Discussion == null)
          {
              return Problem("Entity set 'TinderContext.Discussion'  is null.");
          }
            _context.Discussion.Add(discussion);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiscussionExists(discussion.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDiscussion", new { id = discussion.Id }, discussion);
        }

        // DELETE: api/Discussions/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDiscussion(string id)
        {
            if (_context.Discussion == null)
            {
                return NotFound();
            }
            var discussion = await _context.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            _context.Discussion.Remove(discussion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscussionExists(string id)
        {
            return (_context.Discussion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
