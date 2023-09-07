using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class FoodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public double AverageRating { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }
        public int Calories { get; set; }
    }
}