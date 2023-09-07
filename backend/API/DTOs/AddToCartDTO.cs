using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AddToCartDTO
    {
        [Required]
        public int FoodId { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}