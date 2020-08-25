using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MrRobotWebshop.Models;
using MrRobotWebshop.ViewModels;

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
            var subCategoryList = await db.SubCategory.Include(s => s.Product).ToListAsync();

            if (!subCategoryList.Any())
            {
                return NotFound("There are no subcategories in the database");
            }

            var viewSubCategoryList = new List<SubCategoryViewModel>();

            foreach (var subCategory in subCategoryList)
            {
                var viewSubCategory = new SubCategoryViewModel()
                {
                    SubCategoryId = subCategory.SubCategoryId,
                    SubCategoryName = subCategory.SubCategoryName,
                    ProductCount = subCategory.Product.Count()
                };

                viewSubCategoryList.Add(viewSubCategory);
            }

            return Ok(viewSubCategoryList);
        }

        // GET: api/SubCategories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategory([FromRoute] int id)
        {
            var subCategory = await db.SubCategory.Include(s => s.Product).FirstOrDefaultAsync(s => s.SubCategoryId == id);

            if (subCategory == null)
            {
                return NotFound("No such subcategory");
            }

            var viewSubCategory = new SubCategoryViewModel()
            {
                SubCategoryId = subCategory.SubCategoryId,
                SubCategoryName = subCategory.SubCategoryName,
                ProductCount = subCategory.Product.Count(),
                Products = new List<ProductViewModel>()
            };

            foreach (var product in subCategory.Product)
            {
                var viewProduct = new ProductViewModel()
                {
                    ProductID = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Price = (decimal)product.Price,
                    ImageUrl = product.ImageUrl
                };

                viewSubCategory.Products.Add(viewProduct);
            }

            return Ok(viewSubCategory);
        }

        // POST: api/SubCategories
        [HttpPost]
        public async Task<IActionResult> PostSubCategory([FromForm] SubCategory subCategory)
        {
            if (db.SubCategory.Any(s => s.SubCategoryName == subCategory.SubCategoryName))
            {
                ModelState.AddModelError(string.Empty, "Subcategory name is already taken");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var targetCategory = db.Category.Find(subCategory.CategoryId);

            if (targetCategory == null)
            {
                return BadRequest("There is no such category so I can't add a subcategory to it");
            }

            subCategory.Category = targetCategory;

            db.SubCategory.Add(subCategory);

            await db.SaveChangesAsync();

            return Ok(string.Format("Subcategory '{0}' has been created", subCategory.SubCategoryName));
        }

        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory([FromRoute] int id)
        {
            var subCategory = await db.SubCategory.Include(s => s.Product).FirstOrDefaultAsync(s => s.SubCategoryId == id);

            if (subCategory == null)
            {
                return NotFound("There is no such subcategory");
            }

            db.Product.RemoveRange(subCategory.Product);

            db.SubCategory.Remove(subCategory);

            await db.SaveChangesAsync();

            return Ok(string.Format("Subcategory '{0}' has been deleted", subCategory.SubCategoryName));
        }

        // PUT: api/SubCategories
        [HttpPut]
        public async Task<IActionResult> PutSubCategory([FromForm] SubCategory subCategory)
        {
            if (db.SubCategory.Any(s => s.SubCategoryName == subCategory.SubCategoryName && s.SubCategoryId != subCategory.SubCategoryId))
            {
                ModelState.AddModelError(string.Empty, "Subcategory name is already taken");
            }

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

            return Ok(string.Format("Subcategory '{0}' has been modified", subCategory.SubCategoryName));
        }
    }
}