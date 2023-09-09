using System.Security.Claims;
using API.DTOs;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrdersForUser()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = await _service.GetAllOrdersForUser(username);
            return Ok(orders);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var orders = await _service.GetAllOrders();
            return Ok(orders);
        }
        
        
    }
}