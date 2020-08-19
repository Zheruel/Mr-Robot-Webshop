using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrRobotWebshop.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MrRobotWebshop.ViewModels;
using System.Data;
using System.Text;

namespace MrRobotWebshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebshopUsersController : ControllerBase
    {
        private readonly MrRobotWebshopDBContext db = new MrRobotWebshopDBContext();

        // GET: api/WebshopUsers
        [HttpGet]
        public async Task<IActionResult> GetWebshopUsers()
        {
            var userList = await db.WebshopUser.ToListAsync();

            if(!userList.Any())
            {
                return NotFound("There are no users in the database");
            }

            return Ok(userList);
        }

        // GET: api/WebshopUsers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWebshopUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var webshopUser = await db.WebshopUser.FindAsync(id);

            if (webshopUser == null)
            {
                return NotFound("There is no user with the specified ID");
            }

            return Ok(webshopUser);
        }

        // POST: api/WebshopUsers
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginUser)
        {
            WebshopUser webShopUser = new WebshopUser();

            try
            {
                webShopUser = await db.WebshopUser.Single(s => s.Username == loginUser.Username);
            }

            catch(InvalidOperationException)
            {
                ModelState.AddModelError(string.Empty, "User doesn't exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //hash password
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: loginUser.Password,
                salt: Convert.FromBase64String(webShopUser.Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            if (hashed == webShopUser.Password)
            {
                return Ok("Login successfull");
            }

            else
            {
                return BadRequest("Login failed");
            }
        }

        // POST: api/WebshopUsers
        [HttpPost]
        public async Task<IActionResult> PostWebshopUser([FromForm] WebshopUser webshopUser)
        {
            if (db.WebshopUser.Any(s => s.Username == webshopUser.Username))
            {
                ModelState.AddModelError(string.Empty, "Username is already taken");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // create salt
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            //hash password
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: webshopUser.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            //add salt and hashes password to database
            webshopUser.Salt = Convert.ToBase64String(salt);
            webshopUser.Password = hashed;

            db.WebshopUser.Add(webshopUser);

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return new ObjectResult("Internal server error - Database related problem") { StatusCode = 500 };
            }

            return CreatedAtAction("GetWebshopUser", new { id = webshopUser.WebshopUserId }, webshopUser);
        }

        // DELETE: api/WebshopUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebshopUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var webshopUser = await db.WebshopUser.FindAsync(id);

            if (webshopUser == null)
            {
                return NotFound("There is no user with the specified ID");
            }

            db.WebshopUser.Remove(webshopUser);

            await db.SaveChangesAsync();

            return Ok("Korisnik je uspjesno obrisan");
        }

        // PUT: api/WebshopUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebshopUser([FromRoute] int id, [FromBody] WebshopUser webshopUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webshopUser.WebshopUserId)
            {
                return BadRequest();
            }

            db.Entry(webshopUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebshopUserExists(id))
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

        private bool WebshopUserExists(int id)
        {
            return db.WebshopUser.Any(e => e.WebshopUserId == id);
        }
    }
}