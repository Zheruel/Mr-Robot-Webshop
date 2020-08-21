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
    public class CategoriesController : ControllerBase
    {
        private readonly MrRobotWebshopDBContext db = new MrRobotWebshopDBContext();

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categoryList = await db.Category.ToListAsync();

            if (!categoryList.Any())
            {
                return NotFound("There are no categories in the database");
            }

            return Ok(categoryList);
        }

        // GET: api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var category = await db.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound("There is no such category");
            }

            return Ok(category);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromForm] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Category.Add(category);

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return new ObjectResult("Internal server error - Database related problem") { StatusCode = 500 };
            }

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await db.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound("There is no such category");
            }

            db.Category.Remove(category);

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                return new ObjectResult("Internal server error - Database related problem") { StatusCode = 500 };
            }

            return Ok("Category deleted");
        }

        // PUT: api/Categories
        [HttpPut]
        public async Task<IActionResult> PutCategory([FromForm] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (db.Category.Any(s => s.CategoryId == category.CategoryId))
                {
                    return NotFound("There is no such category");
                }

                else
                {
                    throw;
                }
            }

            return Ok("Category has been modified");
        }
    }
}