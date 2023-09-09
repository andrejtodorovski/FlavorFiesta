using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class FoodStatsDTO
    {
        public FoodDTO Food { get; set; }
        public int NumberOfOrders { get; set; }
    }
}