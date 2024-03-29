using API.Data;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext _context;

        public CartItemRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CartItem>> GetCartItems(int cartId)
        {
            return await _context.CartItems.Where(ci => ci.ShoppingCartId == cartId).ToListAsync();
        }
    }
}