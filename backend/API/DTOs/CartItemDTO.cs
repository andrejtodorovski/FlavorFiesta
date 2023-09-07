using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
        public class CartItemDTO
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public int Subtotal { get; set; }
            public FoodDTO Food { get; set; }
        }
}