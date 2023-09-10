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
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        public FoodService(IFoodRepository repository, ICategoryRepository categoryRepository, IShoppingCartRepository shoppingCartRepository, ICartItemRepository cartItemRepository, IUserRepository userRepository, IReviewRepository reviewRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _cartItemRepository = cartItemRepository;
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
        }   
        public void AddFood(Food food)
        {
            _repository.AddFood(food);
        }

        public async void AddToCart(AddToCartDTO addToCartDTO, string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            var cartItem = new CartItem
            {
                FoodId = addToCartDTO.FoodId,
                Quantity = addToCartDTO.Quantity
            };
            var food = await _repository.GetFood(addToCartDTO.FoodId);
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserId(user.Id);
            var subtotal = cartItem.Quantity * food.Price;
            shoppingCart.TotalPrice += subtotal;
            _cartItemRepository.Create(new CartItem
            {
                Quantity = addToCartDTO.Quantity,
                Subtotal = subtotal,
                FoodId = addToCartDTO.FoodId,
                Food = food,
                ShoppingCartId = shoppingCart.Id,
                ShoppingCart = shoppingCart
            });
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

        public Task<FoodDTO> GetFood(int id)
        {
            return Task.FromResult(_repository.ConvertFoodToDTO(id));
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            return await _repository.GetFoods();
        }

        public async Task<IEnumerable<Food>> GetFoodsByCategory(int categoryId)
        {
            var foods = await _repository.GetFoods();
            return foods.Where(f => f.Categories.Any(c => c.Id == categoryId));
        }

        public async Task<IEnumerable<Food>> GetMostViewedFoods()
        {
            var foods = await _repository.GetFoods();
            return foods.OrderByDescending(f => f.ViewCount).Take(4);
        }

        public async Task<IEnumerable<Food>> GetNewestFoods()
        {
            var foods = await _repository.GetFoods();
            return foods.OrderByDescending(f => f.DateCreated).Take(4);
        }

        public async Task<IEnumerable<Food>> GetTopRatedFoods()
        {
            var foods = await _repository.GetFoods();
            return foods.OrderByDescending(f => f.AverageRating).Take(4);
        }

        public async Task<bool> IsFoodInUserShoppingCart(int foodId, string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserId(user.Id);
            return shoppingCart.CartItems.Any(ci => ci.FoodId == foodId);
        }

        public Task<bool> IsFoodReviewedByUser(int foodId, string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            return Task.FromResult(_reviewRepository.GetReviewsForUser(user.Id).Any(r => r.FoodId == foodId));
        }

        public async Task<Review> LeaveReview(ReviewDTO reviewDTO, int foodId, string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            var food = await _repository.GetFood(foodId);
            var review = new Review
            {
                Rating = reviewDTO.Rating.ToString(),
                Comment = reviewDTO.Comment,
                DateReviewed = DateOnly.FromDateTime(DateTime.Now),
                AppUserId = user.Id,
                AppUser = user,
                FoodId = foodId,
                Food = food
            };
            var reviewAdded = await _reviewRepository.AddReview(review);
            food.AverageRating = _reviewRepository.GetReviewsForFood(foodId).Average(r => int.Parse(r.Rating));
            _repository.UpdateFood(food);
            return await Task.FromResult(reviewAdded);
        }

        public void UpdateFood(Food food)
        {
            _repository.UpdateFood(food);
        }
    }
}