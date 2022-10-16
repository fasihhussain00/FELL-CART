using CartAPI.Application.IService;
using CartAPI.Infrastructure.Exceptions;
using CartAPI.Presentation.ViewModel.Product.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CartAPI.Presentation.Controllers
{
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            var createdProduct = await _productService.Create(product.ToDomain());
            var responseBody = ResponseBuilder.BuildResponse(createdProduct, "Product Created Successfully", 201);
            return Ok(responseBody);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var updatedProduct = await _productService.Update(product.ToDomain());
            var responseBody = ResponseBuilder.BuildResponse(updatedProduct, "Product updated successfully", 200);
            return Ok(responseBody);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid productid)
        {
            if (productid == Guid.Empty)
                return ValidationProblem();
            var deletedProduct = await _productService.Delete(productid);
            var responseBody = ResponseBuilder.BuildResponse(deletedProduct, "Product deleted successfully", 200);
            return Ok(responseBody);
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] Guid? productid = null, [FromQuery] string category = null)
        {
            var customer = await _productService.Get(productid ?? Guid.Empty, category);
            var responseBody = ResponseBuilder.BuildResponse(customer, "Product retrieved successfully", 200);
            return Ok(responseBody);
        }
    }
}
