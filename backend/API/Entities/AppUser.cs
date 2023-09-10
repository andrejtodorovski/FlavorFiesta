namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }     
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public ICollection<Review> Reviews { get; } = new List<Review>();
        public ICollection<ShoppingCart> ShoppingCarts { get; } = new List<ShoppingCart>();
        public ICollection<Order> Orders { get; } = new List<Order>();
        public string PhotoUrl { get; set;}
    }
}