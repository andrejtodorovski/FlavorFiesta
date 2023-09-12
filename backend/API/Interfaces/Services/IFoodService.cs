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
        Task<IEnumerable<FoodDTO>> GetFoodsForMenu();
        Task<FoodDTO> GetFood(int id);
        void AddFood(Food food);
        void UpdateFood(Food food);
        void DeleteFood(int Id);
        Task<IEnumerable<Food>> GetFoodsByCategory(int categoryId);
        Task<IEnumerable<Food>> GetTopRatedFoods();
        Task<IEnumerable<Food>> GetMostViewedFoods();
        Task<IEnumerable<Food>> GetNewestFoods();
        void AddToCart(AddToCartDTO addToCartDTO, string username);
        Task<IEnumerable<Category>> GetCategories();
        Task<bool> IsFoodInUserShoppingCart(int foodId, string username);
        Task<bool> IsFoodReviewedByUser(int foodId, string username);
        Task<Review> LeaveReview(ReviewDTO reviewDTO, int foodId, string username);
        Task<IEnumerable<ReviewInfo>> GetReviewsForFood(int foodId);

    }
}