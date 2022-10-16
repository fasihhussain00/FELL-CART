using CartAPIEntityFramwork.Context;
using CartAPIEntityFramwork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CartAPIEntityFramwork.Controllers
{
    public class CustomersController : CustomBaseController
    {
        public CustomersController(CartDbContext context) : base(context)
        {

        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? id)
        {
            if (id == null)
                return Ok(_context.Customers.AsQueryable());
            if (id != null)
                return Ok(await _context.Customers.FirstOrDefaultAsync(m => m.Id == id));

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ViewModels.Customer customer)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            customer.Id = Guid.NewGuid();
            _context.Customers.Add(MapToModel(customer));
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ViewModels.Customer customer)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            try
            {
                _context.Customers.Update(MapToModel(customer));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CustomerExists(customer.Id))
                    throw;
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        private Customer MapToModel(ViewModels.Customer customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Password = customer.Password
            };
        }
    }
}
