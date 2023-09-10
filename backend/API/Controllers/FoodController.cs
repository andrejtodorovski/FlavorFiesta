using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Interfaces.Services;
using API.DTOs;
using System.Security.Claims;

namespace API.Controllers
{
    public class FoodController : BaseApiController
    {
        private readonly IFoodService _service;
        public FoodController(IFoodService service)
        {
            _service = service;
        }        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods(int categoryId)
        {
            var foods = await _service.GetFoods();
            return Ok(foods);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodDTO>> GetFood(int id)
        {
            var food = await _service.GetFood(id);
            if(food == null) return NotFound();
            return Ok(food);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoodsByCategory(int categoryId)
        {
            var foods = await _service.GetFoodsByCategory(categoryId);
            return Ok(foods);
        }
        [HttpGet("top-rated")]
        public async Task<ActionResult<IEnumerable<Food>>> GetTopRatedFoods()
        {
            var foods = await _service.GetTopRatedFoods();
            return Ok(foods);
        }
        [HttpGet("most-viewed")]
        public async Task<ActionResult<IEnumerable<Food>>> GetMostViewedFoods()
        {
            var foods = await _service.GetMostViewedFoods();
            return Ok(foods);
        }
        [HttpGet("newest")]
        public async Task<ActionResult<IEnumerable<Food>>> GetNewestFoods()
        {
            var foods = await _service.GetNewestFoods();
            return Ok(foods);
        }
        [HttpPost]
        public void AddFood(Food food)
        {
            _service.AddFood(food);
        }
        [HttpPut]
        public void UpdateFood(int FoodId, Food food)
        {
            _service.UpdateFood(food);
        }
        [HttpDelete]
        public void DeleteFood(int Id)
        {
            _service.DeleteFood(Id);
        }
        [HttpPost("add-to-cart")]
        public void AddToCart(AddToCartDTO addToCartDTO)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _service.AddToCart(addToCartDTO, username);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _service.GetCategories();
            return Ok(categories);
        }
        [HttpGet("is-food-in-user-shopping-cart/{id}")]
        public async Task<ActionResult<bool>> IsFoodInUserShoppingCart(int id)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isFoodInUserShoppingCart = await _service.IsFoodInUserShoppingCart(id, username);
            return Ok(isFoodInUserShoppingCart);
        }
        [HttpGet("is-food-reviewed-by-user/{id}")]
        public async Task<ActionResult<bool>> IsFoodReviewedByUser(int id)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isFoodReviewedByUser = await _service.IsFoodReviewedByUser(id, username);
            return Ok(isFoodReviewedByUser);
        }
        [HttpPost("leave-review/{id}")]
        public async Task<ActionResult> LeaveReview(int id, ReviewDTO reviewDTO)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _service.LeaveReview(reviewDTO, id, username);
            return Ok();
        }
    }
}