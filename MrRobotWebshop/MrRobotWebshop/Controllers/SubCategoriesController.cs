using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MrRobotWebshop.Models;

namespace MrRobotWebshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly MrRobotWebshopDBContext db = new MrRobotWebshopDBContext();

        // GET: api/SubCategories
        [HttpGet]
        public async Task<IActionResult> GetSubCategory()
        {
            var subCategoryList = await db.SubCategory.Include(s => s.Category).ToListAsync();

            return Ok(subCategoryList);
        }

        // GET: api/SubCategories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategory([FromRoute] int id)
        {
            var subCategory = await db.SubCategory.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound("No such subcategory");
            }

            return Ok(subCategory);
        }

        // POST: api/SubCategories
        [HttpPost]
        public async Task<IActionResult> PostSubCategory([FromForm] SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var targetCategory = db.Category.Find(subCategory.CategoryId);

            if (targetCategory == null)
            {
                return BadRequest("There is no such category");
            }

            subCategory.Category = targetCategory;

            db.SubCategory.Add(subCategory);

            await db.SaveChangesAsync();

            return Ok("Added");
        }

        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] int id)
        {
            var subCategory = await db.SubCategory.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound("There is no such subcategory");
            }

            db.SubCategory.Remove(subCategory);

            await db.SaveChangesAsync();

            return Ok("Subcategory removed");
        }

        // PUT: api/SubCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategory([FromForm] SubCategory subCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(subCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!db.SubCategory.Any(e => e.SubCategoryId == subCategory.SubCategoryId))
                {
                    return NotFound("There is no such subcategory");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Subcategory has been modified");
        }
    }
}