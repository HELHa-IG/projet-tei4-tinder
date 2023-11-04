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
        [Authorize(Roles = "string")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
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
        public async Task<IActionResult> DeleteUsers(int id)
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

            _context.Users.Remove(users);
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
                Token = "",
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

        public TinderContext Get_context()
        {
            return _context;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login model, TinderContext _context)
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
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Birthday",user.Birthday.ToString("yyyy-MM-dd")),
                    new Claim("Hobbys", user.Hobbys),
                    new Claim("PhotosJson", user.PhotoJson),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
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

            user.Token = encrypterToken;
            user.TokenCreated = DateTime.UtcNow;
            user.TokenExpires = (DateTime)tokenDescriptor.Expires;

            if (_context.Users == null)
            {
                return Problem("Entity set 'AuthentificationTinderContext.Users'  is null.");
            }
            _context.Users.Update(user);
            _context.SaveChanges();

            return new { token = encrypterToken, username = user.FirstName };
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
