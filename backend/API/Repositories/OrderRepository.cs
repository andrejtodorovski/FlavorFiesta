using API.Data;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

            public async Task<IEnumerable<Order>> GetAllOrdersForUser(int userId)
            {
                return await _context.Orders.Where(o => o.AppUserId == userId).ToListAsync();
            }
    }
}