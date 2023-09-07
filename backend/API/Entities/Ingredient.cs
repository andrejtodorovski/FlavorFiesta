namespace API.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FoodIngredient> FoodIngredients { get; } = new();

    }
}