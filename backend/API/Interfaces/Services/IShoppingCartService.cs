using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Interfaces.Services
{
    public interface IShoppingCartService
    {
        public Task<ShoppingCartDTO> GetShoppingCartForUserAsync(int userId);
        public Task<ShoppingCartDTO> GetShoppingCartById(int Id);
        void PlaceOrder(int userId, string phoneNumber, string address);
    }
}