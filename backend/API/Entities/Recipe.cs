namespace API.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfServings { get; set; }
        public string Difficulty { get; set; }
        public int PreparationTime { get; set; }
        public double AverageRating { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}