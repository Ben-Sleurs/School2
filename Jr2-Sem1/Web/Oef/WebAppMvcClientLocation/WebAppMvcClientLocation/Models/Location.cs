using System.ComponentModel.DataAnnotations;
using WebAppMvcClientLocation.CustomModelValidation;

namespace WebAppMvcClientLocation.Models
{
    public class Location
    {
        public int? LocationId { get; set; }
        [Required]
        [CustomPostcode]
        public string? Postcode { get; set; }
        [Required]
        public string? City { get; set; }
    }
}
