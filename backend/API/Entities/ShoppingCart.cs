namespace API.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int TotalPrice { get; set; }        
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = null!;
    }
}