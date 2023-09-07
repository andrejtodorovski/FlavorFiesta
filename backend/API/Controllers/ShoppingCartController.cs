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
            var shoppingCart = await _service.GetShoppingCartForUserAsync(1);
            return Ok(shoppingCart);
        }

        
    }
}