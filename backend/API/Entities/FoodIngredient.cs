using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class FoodIngredient
    {
        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}