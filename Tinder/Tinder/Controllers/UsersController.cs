using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tinder.Data;
using Tinder.Models;

namespace Tinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TinderContext _context;
        private readonly AppSettings _applicationSettings;

        public UsersController(TinderContext context, IOptions<AppSettings> _applicationSettings)
        {
            this._applicationSettings = _applicationSettings.Value;
            _context = context;
        }

        // GET: api/Users
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.Include(model => model.Locality).ToListAsync();
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUsers(int id, [FromBody] Register model)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = model.FirstName;
            existingUser.LastName = model.LastName;
            existingUser.Birthday = model.Birthday;
            existingUser.Hobbys = model.Hobbys;
            existingUser.PhotoJson = model.PhotoJson;
            existingUser.Email = model.Email;
            existingUser.Role = model.Role;

            if(model.Locality != null)
            {
                var existingLocality = _context.Locality.FirstOrDefault(l => l.Equals(model.Locality));

                if (existingLocality != null)
                {
                    existingUser.Locality = existingLocality;
                }
                else
                {
                    // Si elle n'existe pas, vous pouvez créer une nouvelle "locality"
                    existingUser.Locality = model.Locality;
                }
            }
            if (model.ConfirmPassword == model.Password)
            {
                using HMACSHA512? hmac = new();
                existingUser.PasswordSalt = hmac.Key;
                existingUser.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
            }
            else
            {
                return BadRequest("Passwords Don't Match");
            }

            try
            {
                _context.Entry(existingUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {

                if (!UsersExists(id))
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

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            if (_context.Questions == null)
            {
                return NotFound();
            }

            if (_context.MatchLike == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Récupérer toutes les MatchLike associées à l'utilisateur
            var matchLikesToRemove = _context.MatchLike.Where(ml => ml.IdUser01 == id || ml.IdUser02 == id).ToList();
            _context.MatchLike.RemoveRange(matchLikesToRemove); // Supprimer toutes les MatchLike associées

            // Récupérer toutes les questions associées à l'utilisateur
            var questionsToDelete = _context.Questions.Where(q => q.IdUser == id);
            _context.Questions.RemoveRange(questionsToDelete); // Supprimer toutes les questions associées

            _context.Users.Remove(user); // Supprimer l'utilisateur
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpPost("Register")]
        public async Task<ActionResult<Users>> Register([FromBody] Register model)
        {
            var user = new Users
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birthday = model.Birthday,
                Hobbys = model.Hobbys,
                PhotoJson = model.PhotoJson,
                Email = model.Email,
                Role = model.Role,
                Locality = model.Locality,
                
            };

            if (model.ConfirmPassword == model.Password)
            {
                using HMACSHA512? hmac = new();
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
            }
            else
            {
                return BadRequest("Passwords Dont Match");
            }

            if (_context.Users == null)
            {
                return Problem("Entity set 'AuthentificationTinderContext.Users'  is null.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.Id }, user);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login model)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AuthentificationTinderContext.Users'  is null.");
            }
            var user = _context.Users.Where(x => x.Email == model.Email).FirstOrDefault();

            if (user == null)
            {
                return BadRequest("Username Or Password Was Invalid");
            }

            var match = CheckPassword(model.Password, user);

            if (!match)
            {
                return BadRequest("Username Or Password Was Invalid");
            }
            //JWTGenerator(user);
            return Ok(JWTGenerator(user));

        }

        private dynamic JWTGenerator(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._applicationSettings.Secret);

            if (_context.Users == null)
            {
                return Problem("Entity set 'AuthentificationTinderContext.Users'  is null.");
            }

            var userWithLocality = _context.Users.Include(u => u.Locality).Where(u => u.Id == user.Id).FirstOrDefault();

            if (userWithLocality == null)
            {
                return Problem("Error with userWithLocality");
            }



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Birthday", user.Birthday.ToString("yyyy-MM-dd")),
                    new Claim("Hobbys", user.Hobbys),
                    new Claim("PhotosJson", user.PhotoJson),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("LocalityI_Id", userWithLocality.Locality.Id.ToString()),
                    new Claim("Country", userWithLocality.Locality.Pays),
                    new Claim("Region", userWithLocality.Locality.Province),
                    new Claim("City", userWithLocality.Locality.Ville),
                    new Claim("PostalCode", userWithLocality.Locality.CodePostal),
                    new Claim("Street", userWithLocality.Locality.Rue),
                    new Claim("Number", userWithLocality.Locality.Numero),
                    new Claim("Longitude", userWithLocality.Locality.Longitude), 
                    new Claim("Latitude", userWithLocality.Locality.Latitude) 

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encrypterToken = tokenHandler.WriteToken(token);

            HttpContext.Response.Cookies.Append("token", encrypterToken,
                 new CookieOptions
                 {
                     Expires = DateTime.Now.AddDays(7),
                     HttpOnly = true,
                     Secure = true,
                     IsEssential = true,
                     SameSite = SameSiteMode.None
                 });

            return new { token = encrypterToken, username = user.FirstName };
        }

        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            return Ok();
        }
        private bool CheckPassword(string password, Users user)
        {
            bool result;

            using (HMACSHA512? hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = compute.SequenceEqual(user.PasswordHash);
            }

            return result;
        }
    }
}
