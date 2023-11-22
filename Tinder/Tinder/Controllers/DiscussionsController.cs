﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public async Task<ActionResult<IEnumerable<Discussion>>> GetDiscussion()
        {
          if (_context.Discussion == null)
          {
              return NotFound();
          }
            return await _context.Discussion.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsersFromDiscussion(string id)
        {
            // Récupérer la discussion
            var discussion = await _context.Discussion.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            // Récupérer tous les utilisateurs où votre ID est présent
            var discussions = await _context.Discussion
                .Where(d => d.IdUser01 == id || d.IdUser02 == id)
                .ToListAsync();

            // Filtrer pour prendre les ID qui sont différents de l'ID passé en paramètre
            var userIds = discussions
                .Select(d => d.IdUser01 == id ? d.IdUser02 : d.IdUser01)
                .Distinct()
                .ToList();

            // Récupérer dans la table users les noms des user qui ont l'id de ma liste
            if (_context.Users == null)
            {
                return NotFound("Users not found");
            }

            var users = await _context.Users
                .Where(u => userIds.Contains(u.Id.ToString()))
                .ToListAsync();

            // Retourner l'id et le nom de chaque utilisateur
            return users;
        }


        // GET: api/Discussions/5
        [HttpGet("{id}")]
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
