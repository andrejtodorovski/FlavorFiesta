using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public List<FoodIngredient> FoodIngredients { get; } = new();

    }
}