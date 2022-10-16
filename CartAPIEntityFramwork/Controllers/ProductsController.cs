using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CartAPIEntityFramwork.Models;
using CartAPIEntityFramwork.Context;

namespace CartAPIEntityFramwork.Controllers
{
    public class ProductsController : CustomBaseController
    {

        public ProductsController(CartDbContext context) : base(context)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? id = null, [FromQuery] string category = null)
        {
            if (id == null && category == null)
                return Ok(_context.Products.AsQueryable());

             if(category == null)
                return Ok(await _context.Products.FirstOrDefaultAsync(m => m.Id == id));

            if (id == null)
                return Ok(await _context.Products.FirstOrDefaultAsync(m => m.CategoryNavigation.Category1 == category));

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            product.Id = Guid.NewGuid();
            product.Category = await FindOrCreateCategory(product.CategoryNavigation.Category1);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Product product)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            
            try
            {
                product.Category = await FindOrCreateCategory(product.CategoryNavigation.Category1);
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (ProductExists(product.Id))
                    throw;
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        private bool CategoryExists(string Name)
        {
            return _context.Categories.Any(e => e.Category1 == Name);
        }

        private async Task<Guid> CreateCategory(string categoryName)
        {
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Category1 = categoryName
            };
            await _context.Categories.AddAsync(category);
            return category.Id;
        }
        
        private async Task<Guid> FindOrCreateCategory(string categoryName)
        {
            if (!CategoryExists(categoryName))
                return await CreateCategory(categoryName);
            return _context.Categories.Where(c => c.Category1 == categoryName).FirstOrDefault().Id;
        }
    }
}
