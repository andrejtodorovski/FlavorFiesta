using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        public FoodService(IFoodRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }   
        public void AddFood(Food food)
        {
            _repository.AddFood(food);
        }

        public void AddToCart(AddToCartDTO addToCartDTO)
        {
            var cartItem = new CartItem
            {
                FoodId = addToCartDTO.FoodId,
                Quantity = addToCartDTO.Quantity
            };
            // finish this later
        }

        public async void DeleteFood(int Id)
        {
            var food = await _repository.GetFood(Id);
            _repository.DeleteFood(food);
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public async Task<Food> GetFood(int id)
        {
            return await _repository.GetFood(id);
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            return await _repository.GetFoods();
        }

        public async Task<IEnumerable<Food>> GetFoodsByCategory(string category)
        {
            var foods = await _repository.GetFoods();
            return foods.Where(f => f.Categories.Any(c => c.Name == category));
        }

        public async Task<IEnumerable<Food>> GetMostViewedFoods()
        {
            var foods = await _repository.GetFoods();
            return foods.OrderByDescending(f => f.ViewCount).Take(5);
        }

        public async Task<IEnumerable<Food>> GetNewestFoods()
        {
            var foods = await _repository.GetFoods();
            return foods.OrderByDescending(f => f.DateCreated).Take(5);
        }

        public async Task<IEnumerable<Food>> GetTopRatedFoods()
        {
            var foods = await _repository.GetFoods();
            return foods.OrderByDescending(f => f.AverageRating).Take(5);
        }

        public void UpdateFood(Food food)
        {
            _repository.UpdateFood(food);
        }
    }
}