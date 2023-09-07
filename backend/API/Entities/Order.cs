namespace API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string OrderStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = null!;
    }
}