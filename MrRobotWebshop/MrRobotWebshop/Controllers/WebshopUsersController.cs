using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrRobotWebshop.Models;

namespace MrRobotWebshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebshopUsersController : ControllerBase
    {
        private readonly MrRobotWebshopDBContext db = new MrRobotWebshopDBContext();

        // GET: api/WebshopUsers
        [HttpGet]
        public IActionResult GetWebshopUsers()
        {
            var userList = db.WebshopUser;

            if(userList.Count() == 0)
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
        [HttpPost]
        public async Task<IActionResult> PostWebshopUser([FromForm] WebshopUser webshopUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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