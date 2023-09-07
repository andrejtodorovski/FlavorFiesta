using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int TotalPrice { get; set; }        
        public int AppUserId { get; set; }
        public string AppUserName { get; set; }
        public ICollection<CartItemDTO> CartItems { get; set; }
    }
}