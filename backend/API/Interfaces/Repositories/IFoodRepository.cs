using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetFoods();
        Task<Food> GetFood(int id);
        void AddFood(Food food);
        void UpdateFood(Food food);
        void DeleteFood(Food food);
    }
}