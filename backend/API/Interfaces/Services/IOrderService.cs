using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersForUser(string username);
        Task<IEnumerable<OrderDTO>> GetAllOrders();
        Task<Order> UpdateOrderStatusToFinished(int orderId);
        Task<OrderDTO> GetOrderById(int orderId);
    }
}