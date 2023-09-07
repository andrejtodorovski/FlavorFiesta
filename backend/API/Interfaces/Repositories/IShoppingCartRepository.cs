using API.DTOs;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCartDTO> GetShoppingCartById(int Id);      

        Task<ShoppingCartDTO> GetShoppingCartForUserAsync(int userId);      
        void PlaceOrder(int userId, string phoneNumber, string address);

        ShoppingCart Create(AppUser user);
        Task<ShoppingCart> GetShoppingCartByUserId(int userId);
    }
}