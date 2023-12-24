using System;
using System.Collections.Generic;
using System.Globalization;
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




        [HttpGet("{id}/{radius}")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsersInRadius(int id, double radius)
        {
            if (_context.Locality == null || _context.Users == null || _context.MatchLike == null)
            {
                return NotFound();
            }

            // Récupérer la localité spécifiée
            var locality = await _context.Locality.FindAsync(id);
            if (locality == null)
            {
                return NotFound();
            }

            // Récupérer tous les utilisateurs
            var allUsers = await _context.Users.Include(u => u.Locality).ToListAsync();

            // Filtrer les utilisateurs qui sont dans la table MatchLike
            var likedUsers = await _context.MatchLike
                .Where(m => (m.User01Like && m.IdUser01 == id) || (m.User02Like && m.IdUser02 == id))
                .Select(m => m.IdUser01 == id ? m.IdUser02 : m.IdUser01)
                .ToListAsync();

            // Filtrer les utilisateurs qui ne sont pas aimés et qui sont dans le rayon spécifié
            var usersInRadius = allUsers
                .Where(u => !likedUsers.Contains(u.Id) && CalculDistanceBetweenTwoPoints(locality.Latitude, locality.Longitude, u.Locality.Latitude, u.Locality.Longitude) <= radius && u.Id != id)
                .ToList();

            return usersInRadius;
        }


        private double CalculDistanceBetweenTwoPoints(string lat1, string lon1, string lat2, string lon2)
        {
            double R = 6371; // Rayon de la Terre en kilomètres
            double dLat1, dLon1, dLat2, dLon2;

            try
            {
                dLat1 = double.Parse(lat1, CultureInfo.InvariantCulture);
                dLon1 = double.Parse(lon1, CultureInfo.InvariantCulture);
                dLat2 = double.Parse(lat2, CultureInfo.InvariantCulture);
                dLon2 = double.Parse(lon2, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                // La conversion a échoué, gérer l'erreur selon vos besoins
                return -1;
            }

            double phi1 = Math.PI / 180 * dLat1;
            double phi2 = Math.PI / 180 * dLat2;
            double deltaPhi = Math.PI / 180 * (dLat2 - dLat1);
            double deltaLambda = Math.PI / 180 * (dLon2 - dLon1);

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                       Math.Cos(phi1) * Math.Cos(phi2) *
                       Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
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
            if (_context.Users == null)
            {
                return Problem("Entity set 'AuthentificationTinderContext.Users'  is null.");
            }

            if (_context.Locality == null)
            {
                return NotFound();
            }
            var locality = await _context.Locality.FindAsync(id);
            if (locality == null)
            {
                return NotFound();
            }

            // Vérifiez s'il y a des utilisateurs associés à cette localité
            if (_context.Users.Any(u => u.LocalityId == id))
            {
                return BadRequest("Suppression de la localité impossible car elle est associée à des utilisateurs.");
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
