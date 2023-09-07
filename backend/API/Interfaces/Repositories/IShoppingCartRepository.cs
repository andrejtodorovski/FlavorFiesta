using API.DTOs;

namespace API.Interfaces.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCartDTO> GetShoppingCartById(int Id);      

        Task<ShoppingCartDTO> GetShoppingCartForUserAsync(int userId);      
        void PlaceOrder(int userId, string phoneNumber, string address);
    }
}