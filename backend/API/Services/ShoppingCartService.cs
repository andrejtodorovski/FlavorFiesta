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
        private readonly IUserRepository _userRepository;
        public ShoppingCartService(IShoppingCartRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        public Task<ShoppingCartDTO> GetShoppingCartById(int Id)
        {
            return _repository.GetShoppingCartById(Id);
        }

        public Task<ShoppingCartDTO> GetShoppingCartForUserAsync(string username)
        {
            var user = _userRepository.GetUserByUsername(username);

            return _repository.GetShoppingCartForUserAsync(user.Id);
        }

        public void PlaceOrder(string username, PlaceOrderDTO placeOrderDTO)
        {
            var user = _userRepository.GetUserByUsername(username);
            _repository.PlaceOrder(user.Id, placeOrderDTO);
            _repository.ShoppingCartUpdates(user.Id, placeOrderDTO);

        }
    }
}