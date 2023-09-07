using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Interfaces.Services;
using API.DTOs;

namespace API.Controllers
{
    public class FoodController : BaseApiController
    {
        private readonly IFoodService _service;
        public FoodController(IFoodService service)
        {
            _service = service;
        }
        // add query param categoryId to this method below
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods(int categoryId)
        {
            var foods = await _service.GetFoods();
            return Ok(foods);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            var food = await _service.GetFood(id);
            if(food == null) return NotFound();
            return Ok(food);
        }
        [HttpGet("category/{category}")]
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
            _service.AddToCart(addToCartDTO);
        }
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _service.GetCategories();
            return Ok(categories);
        }
    }
}