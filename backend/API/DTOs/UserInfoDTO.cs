using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public int NumberOfReviews { get; set; }
        public IEnumerable<OrderDTO> Orders { get; set; }
        public int NumberOfOrders { get; set; }
        public IEnumerable<FoodStatsDTO> MostOrderedFoods { get; set; }

    }
}