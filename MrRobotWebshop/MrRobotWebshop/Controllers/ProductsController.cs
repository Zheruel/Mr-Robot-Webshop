using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MrRobotWebshop.Models;
using MrRobotWebshop.ViewModels;

namespace MrRobotWebshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MrRobotWebshopDBContext db = new MrRobotWebshopDBContext();

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var productList = await db.Product.ToListAsync();

            if (!productList.Any())
            {
                return NotFound("There are no products in the database");
            }

            return Ok(productList);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await db.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound("There is no such product");
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromForm] ProductViewModel viewProduct)
        {
            if (db.Product.Any(s => s.ProductName == viewProduct.ProductName))
            {
                ModelState.AddModelError(string.Empty, "Product name is already taken");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string imageFileName = SaveImage(viewProduct);

            Product product = new Product
            {
                ProductName = viewProduct.ProductName,
                ProductDescription = viewProduct.ProductDescription,
                Price = viewProduct.Price,
                SubCategoryId = viewProduct.SubCategoryId,
                ImageUrl = imageFileName
            };

            db.Product.Add(product);

            await db.SaveChangesAsync();

            return Ok(string.Format("Product '{0}' has been created", viewProduct.ProductName));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await db.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            db.Product.Remove(product);

            await db.SaveChangesAsync();

            return Ok(string.Format("Product '{0}' has been removed", product.ProductName));
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (db.Product.Any(s => s.ProductName == product.ProductName && s.ProductId != product.ProductId))
            {
                ModelState.AddModelError(string.Empty, "Product name is already taken");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.Product.Any(e => e.ProductId == product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(string.Format("Product '{0}' has been modified", product.ProductName));
        }

        private string SaveImage(ProductViewModel viewProduct)
        {
            string uniqueFileName = null;

            if (viewProduct.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");

                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewProduct.ProfileImage.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                viewProduct.ProfileImage.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}