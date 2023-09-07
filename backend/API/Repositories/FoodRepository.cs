using API.Data;
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
            return await _context.Foods.ToListAsync();
        }

        public void UpdateFood(Food food)
        {
            _context.Foods.Update(food);
            _context.SaveChanges();
        }
    }
}