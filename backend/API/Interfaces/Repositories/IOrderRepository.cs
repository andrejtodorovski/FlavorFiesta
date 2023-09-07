using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersForUser(int userId);      
    }
}