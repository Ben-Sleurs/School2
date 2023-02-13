using System.ComponentModel.DataAnnotations;
using WebAppMvcClientLocation.CustomModelValidation;

namespace WebAppMvcClientLocation.Models
{
    public class Client
    {
        public int? ClientId { get; set; }
        public int? LocationId { get; set; }
        [Required]
        [CustomNoNumbers]
        public string? ClientName { get; set; }
    }
}
