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

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(user => user.AppUser).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersForUser(int userId)
        {
            return await _context.Orders.Include(user => user.AppUser).Where(o => o.AppUserId == userId).ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.Include(user => user.AppUser).Include(s => s.ShoppingCart).ThenInclude(cart => cart.CartItems)
            .ThenInclude(item => item.Food).FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var o = _context.Orders.Update(order).Entity;
            await _context.SaveChangesAsync();
            return o;
        }
    }
}