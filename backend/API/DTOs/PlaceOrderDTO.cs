using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class PlaceOrderDTO
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string StripeToken { get; set; }
    }
}