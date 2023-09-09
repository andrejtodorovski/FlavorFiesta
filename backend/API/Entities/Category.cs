using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Food> Foods { get; } = new List<Food>();
    }
}