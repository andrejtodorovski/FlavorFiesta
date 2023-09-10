using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
        public DateOnly DateReviewed { get; set; }
        public int FoodId { get; set; }
        [JsonIgnore]
        public Food Food { get; set; } = null!;
        public int AppUserId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; } = null!;
    }
}