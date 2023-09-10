using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly DataContext _context;

        public FoodRepository(DataContext context)
        {
            _context = context;
        }

        public void AddFood(Food food)
        {
            _context.Foods.Add(food);
            _context.SaveChanges();
        }

        public FoodDTO ConvertFoodToDTO(int id)
        {
            var food = _context.Foods.Find(id);
            var foodIngredients = _context.FoodIngredients.Include(fi=>fi.Ingredient).Where(fi => fi.FoodId == id).ToList();
            var ingredients = foodIngredients.Select(fi => fi.Ingredient).ToList();
            var categories = _context.Categories.Where(c => c.Foods.Any(f => f.Id == id)).ToList();
            var reviews = _context.Reviews.Where(r => r.FoodId == id).ToList();
            var foodDTO = new FoodDTO
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Price = food.Price,
                AverageRating = food.AverageRating,
                ViewCount = food.ViewCount,
                DateCreated = food.DateCreated,
                Calories = food.Calories,
                PhotoUrl = food.PhotoUrl,
                Ingredients = ingredients,
                Categories = categories,
                Reviews = reviews
            };
            return foodDTO;
        }

        public void DeleteFood(Food food)
        {
            _context.Foods.Remove(food);
            _context.SaveChanges();
        }

        public async Task<Food> GetFood(int id)
        {
            return await _context.Foods.FindAsync(id);
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            return await _context.Foods.Include(category => category.Categories).ToListAsync();
        }

        public void UpdateFood(Food food)
        {
            _context.Foods.Update(food);
            _context.SaveChanges();
        }
    }
}