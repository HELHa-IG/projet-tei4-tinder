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
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly TinderContext _context;

        public QuestionsController(TinderContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            return await _context.Questions.ToListAsync();
        }

        // GET: api/Questions/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestions(int id)
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            var questions = await _context.Questions.FindAsync(id);

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(int id, Questions questions)
        {
            if (id != questions.Id)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
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

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestions(Questions questions)
        {
            if (_context.Questions == null)
            {
                return Problem("Entity set 'TinderContext.Questions'  is null.");
            }

            if (_context.Users == null)
            {
                return Problem("Entity set 'TinderContext.Questions'  is null.");
            } 

            var user = await _context.Users.FindAsync(questions.IdUser);

            if (user != null)
            {
                // L'utilisateur existe, vous pouvez insérer la question
                _context.Questions.Add(questions);
                await _context.SaveChangesAsync();
                
            }
            else
            {
                return Problem("IdUser non valide");
            }

            return CreatedAtAction("GetQuestions", new { id = questions.Id }, questions);
        }

        // DELETE: api/Questions/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestions(int id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionsExists(int id)
        {
            return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
