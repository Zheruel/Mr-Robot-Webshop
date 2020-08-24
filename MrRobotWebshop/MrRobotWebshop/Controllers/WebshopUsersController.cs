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
            var userList = await db.WebshopUser.Include(s => s.Receipt).ToListAsync();

            if (!userList.Any())
            {
                return NotFound("There are no users in the database");
            }

            var viewUserList = new List<WebshopUserViewModel>();

            foreach (var user in userList)
            {
                WebshopUserViewModel viewUser = new WebshopUserViewModel()
                {
                    WebshopUserId = user.WebshopUserId,
                    Username = user.Username,
                    Password = user.Password,
                    Salt = user.Salt,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    ReceiptCount = user.Receipt.Count()
                };

                viewUserList.Add(viewUser);
            }

            return Ok(viewUserList);
        }

        // GET: api/WebshopUsers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWebshopUser([FromRoute] int id)
        {
            var webshopUser = await db.WebshopUser.Include(s => s.Receipt).SingleAsync(s => s.WebshopUserId == id);

            if (webshopUser == null)
            {
                return NotFound("There is no such user");
            }

            WebshopUserViewModel viewUser = new WebshopUserViewModel()
            {
                WebshopUserId = webshopUser.WebshopUserId,
                Username = webshopUser.Username,
                Password = webshopUser.Password,
                Salt = webshopUser.Salt,
                Firstname = webshopUser.Firstname,
                Lastname = webshopUser.Lastname,
                ReceiptCount = webshopUser.Receipt.Count()
            };

            return Ok(viewUser);
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

            //add salt and hashed password to database
            webshopUser.Salt = Convert.ToBase64String(salt);
            webshopUser.Password = hashed;

            db.WebshopUser.Add(webshopUser);

            await db.SaveChangesAsync();

            return Ok(string.Format("User '{0}' has been created", webshopUser.Username));
        }

        // DELETE: api/WebshopUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebshopUser([FromRoute] int id)
        {
            var webshopUser = await db.WebshopUser.FindAsync(id);

            if (webshopUser == null)
            {
                return NotFound("There is no user with the specified ID");
            }

            db.WebshopUser.Remove(webshopUser);

            await db.SaveChangesAsync();

            return Ok("User has been deleted");
        }

        // PUT: api/WebshopUsers
        [HttpPut]
        public async Task<IActionResult> PutWebshopUser([FromForm] WebshopUser webshopUser)
        {
            if (db.WebshopUser.Any(s => s.Username == webshopUser.Username && s.WebshopUserId != webshopUser.WebshopUserId))
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

            webshopUser.Salt = Convert.ToBase64String(salt);
            webshopUser.Password = hashed;

            db.Entry(webshopUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!db.WebshopUser.Any(s => s.WebshopUserId == webshopUser.WebshopUserId))
                {
                    return NotFound("There is no such user");
                }

                else
                {
                    throw;
                }
            }

            return Ok("User has been modified");
        }

        // POST: api/WebshopUsers
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginUser)
        {
            WebshopUser webShopUser = new WebshopUser();

            try
            {
                webShopUser = await db.WebshopUser.SingleAsync(s => s.Username == loginUser.Username);
            }

            catch (InvalidOperationException)
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
    }
}