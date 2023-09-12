using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersForUser(int userId);      
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int orderId);
        Task<Order> UpdateOrder(Order order);
    }
}