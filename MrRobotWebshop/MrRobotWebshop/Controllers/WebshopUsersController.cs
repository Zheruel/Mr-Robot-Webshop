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
        public IEnumerable<WebshopUser> GetWebshopUsers()
        {
            return db.WebshopUser.ToList();
        }

        // GET: api/WebshopUsers/5
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
                return NotFound();
            }

            return Ok(webshopUser);
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

        // POST: api/WebshopUsers
        [HttpPost]
        public async Task<IActionResult> PostWebshopUser([FromBody] WebshopUser webshopUser)
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
                if (WebshopUserExists(webshopUser.WebshopUserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
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
                return NotFound();
            }

            db.WebshopUser.Remove(webshopUser);
            await db.SaveChangesAsync();

            return Ok(webshopUser);
        }

        private bool WebshopUserExists(int id)
        {
            return db.WebshopUser.Any(e => e.WebshopUserId == id);
        }
    }
}