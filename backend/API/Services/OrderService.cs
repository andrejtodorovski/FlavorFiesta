using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public OrderService(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }
    
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersForUser(int userId)
        {
            var orders = await _orderRepository.GetAllOrdersForUser(userId);

            return orders.Select(async order => new OrderDTO
            {
                Id = order.Id,
                PhoneNumber = order.PhoneNumber,
                Address = order.Address,
                OrderStatus = order.OrderStatus,
                DateCreated = order.DateCreated,
                AppUserId = order.AppUserId,
                AppUserName = order.AppUser.UserName,
                ShoppingCart = await _shoppingCartRepository.GetShoppingCartById(order.ShoppingCartId)
            }).Select(task => task.Result).ToList();

        }
    }
}