using CartAPI.Application.IService;
using CartAPI.Infrastructure.Exceptions;
using CartAPI.Presentation.ViewModel.Customer.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CartAPI.Presentation.Controllers
{
    public class CustomerController : CustomBaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var createdCustomer = await _customerService.Create(customer.ToDomain());
            var responseBody = ResponseBuilder.BuildResponse(createdCustomer, "Customer Created Successfully", 201);
            return Ok(responseBody);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var updatedCustomer = await _customerService.Update(customer.ToDomain());
            var responseBody = ResponseBuilder.BuildResponse(updatedCustomer, "Customer updated successfully", 200);
            return Ok(responseBody);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer([FromQuery] Guid? customerid)
        {
            if (customerid == null)
                return ValidationProblem();
            var deletedCustomer = await _customerService.Delete(customerid.Value);
            var responseBody = ResponseBuilder.BuildResponse(deletedCustomer, "customer deleted successfully", 200);
            return Ok(responseBody);

        }
        [HttpGet]
        public async Task<IActionResult> GetCustomer([FromQuery] Guid? customerid)
        {
            if (customerid == null)
                return ValidationProblem("customerid cannot be null or empty");
            var customer = await _customerService.Get(customerid.Value);
            var responseBody = ResponseBuilder.BuildResponse(customer, "Customer retrieved successfully", 302);
            return Ok(responseBody);
        }
        [HttpPost("authenticate"), AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromQuery] string email, [FromQuery] string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return ValidationProblem("username and password cannot be null or empty");
            var customer = await _customerService.Authenticate(email, password);
            var responseBody = ResponseBuilder.BuildResponse(customer, "Authenticated Succesfully", 200);
            return Ok(responseBody);
        }
    }
}
