using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces.Services
{
    public interface IFoodService
    {
        Task<IEnumerable<Food>> GetFoods();
        Task<Food> GetFood(int id);
        void AddFood(Food food);
        void UpdateFood(Food food);
        void DeleteFood(int Id);
        Task<IEnumerable<Food>> GetFoodsByCategory(string category);
        Task<IEnumerable<Food>> GetTopRatedFoods();
        Task<IEnumerable<Food>> GetMostViewedFoods();
        Task<IEnumerable<Food>> GetNewestFoods();
        void AddToCart(AddToCartDTO addToCartDTO);
        Task<IEnumerable<Category>> GetCategories();
    }
}