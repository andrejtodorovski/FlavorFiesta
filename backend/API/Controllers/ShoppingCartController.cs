using System.Security.Claims;
using API.DTOs;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _service;
        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCartDTO>> GetShoppingCart()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shoppingCart = await _service.GetShoppingCartForUserAsync(username);
            return Ok(shoppingCart);
        }

        [HttpPost]
        public void PlaceOrder(PlaceOrderDTO placeOrderDTO)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _service.PlaceOrder(username, placeOrderDTO);
        }

        
    }
}