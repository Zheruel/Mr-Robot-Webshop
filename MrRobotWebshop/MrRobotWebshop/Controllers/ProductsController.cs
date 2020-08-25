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
        public async Task<IActionResult> PostProduct([FromForm] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(product);
            await db.SaveChangesAsync();

            return Ok(string.Format("Product '{0}' has been created", product.ProductName));
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
    }
}