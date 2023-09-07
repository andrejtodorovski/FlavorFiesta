namespace API.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public double AverageRating { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
        public int Calories { get; set; }
        public List<FoodIngredient> FoodIngredients { get; } = new();
        public List<Category> Categories { get; } = new();
        public ICollection<Review> Reviews { get; } = new List<Review>();
        public ICollection<CartItem> CartItems { get; } = new List<CartItem>();
    }
}