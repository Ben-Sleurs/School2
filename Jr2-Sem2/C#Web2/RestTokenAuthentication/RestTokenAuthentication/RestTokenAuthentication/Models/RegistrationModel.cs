using System.ComponentModel.DataAnnotations;

namespace RestTokenAuthentication.Models
{
    public class RegistrationModel : LoginModel
    {
        [EmailAddress]   
        [Required]
        public string Email { get; set; }
    }
}
