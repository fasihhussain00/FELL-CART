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
    public class CartsController : CustomBaseController
    {

        public CartsController(CartDbContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid? id = null, Guid? customerid = null)
        {
            if (id == null && customerid == null)
                return ValidationProblem();

            if (id == null)
                return Ok(await _context.Carts.Include(c => c.Customer).FirstOrDefaultAsync(m => m.Customerid == customerid));

            if (customerid == null)
                return Ok(await _context.Carts.Include(c => c.Customer).FirstOrDefaultAsync(m => m.Id == id));

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cart cart)
        {
            if (!ModelState.IsValid || cart.Customerid == null)
                return ValidationProblem();

            cart.Id = Guid.NewGuid();
            _context.Add(cart);
            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Cart cart)
        {
            if (!ModelState.IsValid || cart.Id == Guid.Empty)
                return ValidationProblem();
            try
            {
                _context.Update(cart);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CartExists(cart.Id))
                    throw;
                return NotFound();
            }
            return Ok(cart);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        private bool CartExists(Guid id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
