using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            if (orders == null)
            {
                return Enumerable.Empty<OrderDTO>();
            }

            var orderDTOs = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var orderDTO = new OrderDTO
                {
                    Id = order.Id,
                    PhoneNumber = order.PhoneNumber,
                    Address = order.Address,
                    OrderStatus = order.OrderStatus,
                    DateCreated = order.DateCreated,
                    AppUserId = order.AppUserId,
                    AppUserName = order.AppUser.UserName,
                    ShoppingCart = await _shoppingCartRepository.GetShoppingCartById(order.ShoppingCartId)
                };
                orderDTOs.Add(orderDTO);
            }

            return orderDTOs;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersForUser(string username)
        {
            var user = _userRepository.GetUserByUsername(username);

            var orders = await _orderRepository.GetAllOrdersForUser(user.Id);

            if (orders == null)
            {
                return Enumerable.Empty<OrderDTO>();
            }

            var orderDTOs = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var orderDTO = new OrderDTO
                {
                    Id = order.Id,
                    PhoneNumber = order.PhoneNumber,
                    Address = order.Address,
                    OrderStatus = order.OrderStatus,
                    DateCreated = order.DateCreated,
                    AppUserId = order.AppUserId,
                    AppUserName = order.AppUser.UserName,
                    ShoppingCart = await _shoppingCartRepository.GetShoppingCartById(order.ShoppingCartId)
                };
                orderDTOs.Add(orderDTO);
            }

            return orderDTOs;
        }

        public async Task<OrderDTO> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            var orderDTO = new OrderDTO
            {
                Id = order.Id,
                PhoneNumber = order.PhoneNumber,
                Address = order.Address,
                OrderStatus = order.OrderStatus,
                DateCreated = order.DateCreated,
                AppUserId = order.AppUserId,
                AppUserName = order.AppUser.UserName,
                ShoppingCart = _shoppingCartRepository.GetShoppingCartById(order.ShoppingCartId).Result
            };
            return orderDTO;
        }

        public async Task<Order> UpdateOrderStatusToFinished(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            order.OrderStatus = "Finished";
            return await _orderRepository.UpdateOrder(order);
        }
    }
}