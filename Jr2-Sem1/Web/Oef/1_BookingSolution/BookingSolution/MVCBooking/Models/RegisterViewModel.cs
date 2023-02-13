using System.ComponentModel.DataAnnotations;

namespace MVCBooking.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        //[Display(Description = "Password: please enter 4 digits?")]
        //[Range(0, 9999)]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password doesn't match!")]
        public string ConfirmPassword { get; set; }
    }
}
