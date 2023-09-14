using API.DTOs;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCartDTO> GetShoppingCartById(int Id);      

        Task<ShoppingCartDTO> GetShoppingCartForUserAsync(int userId);      
        Task PlaceOrder(int userId, PlaceOrderDTO placeOrderDTO);

        ShoppingCart Create(AppUser user);
        Task<ShoppingCart> GetShoppingCartByUserId(int userId);
        Task ShoppingCartUpdates(int userId, PlaceOrderDTO placeOrderDTO);
    }
}