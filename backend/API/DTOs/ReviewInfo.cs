using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class ReviewInfo
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
        public DateTime DateReviewed { get; set; }
        public string FoodName { get; set; }
        public string FoodPhotoUrl { get; set; }
        public double FoodAverageRating { get; set; }
        public int FoodId { get; set; }
        public string AppUserName { get; set; }
        public string AppUserPhotoUrl { get; set; }
    }
}