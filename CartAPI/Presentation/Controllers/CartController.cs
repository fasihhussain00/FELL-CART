using CartAPI.Application.IService;
using CartAPI.Infrastructure.Exceptions;
using CartAPI.Presentation.ViewModel.Cart.Input;
using CartAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CartAPI.Presentation.Controllers
{
    public class CartController : CustomBaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] Cart cart)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var createdCart = await _cartService.Create(cart.ToDomain());
            var responseBody = ResponseBuilder.BuildResponse(createdCart, "Cart Created Successfully", 201);
            return Ok(responseBody);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCart([FromBody] Cart cart)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var updatedCart = await _cartService.Update(cart.ToDomain());
            var responseBody = ResponseBuilder.BuildResponse(updatedCart, "Cart updated successfully", 200);
            return Ok(responseBody);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCart([FromQuery] Guid cartid)
        {
            if (cartid == Guid.Empty)
                return ValidationProblem("cartid is guid and it cannot be null or empty", "Guid.cartid", 400, "guid cannot be null", typeof(Guid).ToString());
            var deletedCart = await _cartService.Delete(cartid);
            var responseBody = ResponseBuilder.BuildResponse(deletedCart, "Cart deleted successfully", 200);
            return Ok(responseBody);
        }
        [HttpGet]
        public async Task<IActionResult> GetCart([FromQuery] Guid? cartid = null, [FromQuery] Guid? customerid = null)
        {
            if (cartid == null && customerid == null)
                return ValidationProblem();
            var carts = await _cartService.Get(cartid ?? Guid.Empty, customerid ?? Guid.Empty);
            var responseBody = ResponseBuilder.BuildResponse(carts, "Cart retrieved successfully", 200);
            return Ok(responseBody);
        }
    }
}
