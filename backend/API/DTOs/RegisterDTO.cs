using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        [EmailAddress]
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        [Required]
        public string password { get; set; }
    }
}