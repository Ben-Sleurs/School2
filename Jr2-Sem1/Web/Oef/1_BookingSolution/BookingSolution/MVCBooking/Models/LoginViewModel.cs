using System.ComponentModel.DataAnnotations;

namespace MVCBooking.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
