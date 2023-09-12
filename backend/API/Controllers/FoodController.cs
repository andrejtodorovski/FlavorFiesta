using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Interfaces.Services;
using API.DTOs;
using System.Security.Claims;
using GemBox.Document;
using System.Text;
using System.Linq;

namespace API.Controllers
{
    public class FoodController : BaseApiController
    {
        private readonly IFoodService _service;
        public FoodController(IFoodService service)
        {
            _service = service;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");   
            ComponentInfo.FreeLimitReached += (sender, e) => e.FreeLimitReachedAction = FreeLimitReachedAction.ContinueAsTrial;
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
        [HttpGet("reviews/{id}")]
        public async Task<ActionResult<IEnumerable<ReviewInfo>>> GetReviews(int id)
        {
            var reviews = await _service.GetReviewsForFood(id);
            return Ok(reviews);
        }
        [HttpGet("menu")]
        public async Task<ActionResult> DownloadMenu()
        {
            var foods = await _service.GetFoodsForMenu();
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Menu.docx");
            var document = DocumentModel.Load(templatePath);
            StringBuilder breakfast = new();
            StringBuilder burgers = new();
            StringBuilder pizzas = new();
            StringBuilder desserts = new();
            StringBuilder drinks = new();
            StringBuilder pasta = new();
            foreach (var food in foods)
            {
                if (food.Categories.Any(c=>c.Name=="Breakfast"))
                {
                    breakfast.AppendLine(food.Name + "\n" + food.Price + "MKD \n" + String.Join('/', food.Ingredients.Select(i=>i.Name)) + "\n");
                }
                if (food.Categories.Any(c=>c.Name=="Burgers"))
                {
                    burgers.AppendLine(food.Name + "\n" + food.Price + "MKD \n" + String.Join('/', food.Ingredients.Select(i=>i.Name)) + "\n");
                }
                if (food.Categories.Any(c=>c.Name=="Pizza"))
                {
                    pizzas.AppendLine(food.Name + "\n" + food.Price + "MKD \n" + String.Join('/', food.Ingredients.Select(i=>i.Name)) + "\n");
                }
                if (food.Categories.Any(c=>c.Name=="Desserts"))
                {
                    desserts.AppendLine(food.Name + "\n" + food.Price + "MKD \n" + String.Join('/', food.Ingredients.Select(i=>i.Name)) + "\n");
                }
                if (food.Categories.Any(c=>c.Name=="Drinks"))
                {
                    drinks.AppendLine(food.Name + "\n" + food.Price + "MKD \n" + String.Join('/', food.Ingredients.Select(i=>i.Name)) + "\n");
                }
                if (food.Categories.Any(c=>c.Name=="Pasta"))
                {
                    pasta.AppendLine(food.Name + "\n" + food.Price + "MKD \n" + String.Join('/', food.Ingredients.Select(i=>i.Name)) + "\n");
                }   
            }
            document.Content.Replace("{{Breakfast}}", breakfast.ToString());
            document.Content.Replace("{{Burgers}}", burgers.ToString());
            document.Content.Replace("{{Pizza}}", pizzas.ToString());
            document.Content.Replace("{{Desserts}}", desserts.ToString());
            document.Content.Replace("{{Drinks}}", drinks.ToString());  
            document.Content.Replace("{{Pasta}}", pasta.ToString());
            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "Menu.pdf");
        }
    }
}