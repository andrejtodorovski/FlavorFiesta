using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetCartItems(int cartId);
        void Create(CartItem cartItem);
    }
}