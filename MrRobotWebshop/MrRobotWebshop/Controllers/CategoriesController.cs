using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore;
using MrRobotWebshop.Models;
using MrRobotWebshop.ViewModels;
using MrRobotWebshop.Controllers;

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
            var categoryList = await db.Category.Include(s => s.SubCategory).ToListAsync();

            if (!categoryList.Any())
            {
                return NotFound("There are no categories in the database");
            }

            var viewCategoryList = new List<CategoryViewModel>();

            foreach (var category in categoryList)
            {
                var viewCategory = new CategoryViewModel()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    SubCategoryCount = category.SubCategory.Count()
                };

                viewCategoryList.Add(viewCategory);
            }

            return Ok(viewCategoryList);
        }

        // GET: api/Categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            var category = await db.Category.Include(s => s.SubCategory).SingleOrDefaultAsync(s => s.CategoryId == id);

            if (category == null)
            {
                return NotFound("There is no such category");
            }

            var viewCategory = new CategoryViewModel() 
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                SubCategoryCount = category.SubCategory.Count(),
                SubCategories = new List<SubCategoryViewModel>()
            };

            foreach(var subCategory in category.SubCategory)
            {
                var viewSubCategory = new SubCategoryViewModel()
                {
                    SubCategoryId = subCategory.SubCategoryId,
                    SubCategoryName = subCategory.SubCategoryName
                };

                viewCategory.SubCategories.Add(viewSubCategory);
            }

            return Ok(viewCategory);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromForm] Category category)
        {
            if (db.Category.Any(s => s.CategoryName == category.CategoryName))
            {
                ModelState.AddModelError(string.Empty, "Category name is already taken");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Category.Add(category);

            await db.SaveChangesAsync();

            return Ok(string.Format("Category '{0}' has been created", category.CategoryName));
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

            //remove subcategories linked then category
            db.SubCategory.RemoveRange(db.SubCategory.Where(s => s.CategoryId == id));
            db.Category.Remove(category);

            await db.SaveChangesAsync();

            return Ok(string.Format("Category '{0}' has been deleted", category.CategoryName));
        }

        // PUT: api/Categories
        [HttpPut]
        public async Task<IActionResult> PutCategory([FromForm] Category category)
        {
            if (db.Category.Any(s => s.CategoryName == category.CategoryName && s.CategoryId != category.CategoryId))
            {
                ModelState.AddModelError(string.Empty, "Category name is already taken");
            }

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
                if (!db.Category.Any(s => s.CategoryId == category.CategoryId))
                {
                    return NotFound("There is no such category");
                }

                else
                {
                    throw;
                }
            }

            return Ok(string.Format("Category '{0}' has been modified", category.CategoryName));
        }
    }
}