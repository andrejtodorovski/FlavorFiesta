using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _repository;
        public ShoppingCartService(IShoppingCartRepository repository)
        {
            _repository = repository;
        }
        public Task<ShoppingCartDTO> GetShoppingCartById(int Id)
        {
            return _repository.GetShoppingCartById(Id);
        }

        public Task<ShoppingCartDTO> GetShoppingCartForUserAsync(int userId)
        {
            return _repository.GetShoppingCartForUserAsync(userId);
        }

        public void PlaceOrder(int userId, string phoneNumber, string address)
        {
            _repository.PlaceOrder(userId, phoneNumber, address);
        }
    }
}