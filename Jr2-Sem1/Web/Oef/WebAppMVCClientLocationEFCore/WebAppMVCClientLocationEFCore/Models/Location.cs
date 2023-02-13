using System.ComponentModel.DataAnnotations;
using WebAppMVCClientLocationEFCore.CustomModelValidation;

namespace WebAppMVCClientLocationEFCore.Models
{
    public class Location
    {
        public int? LocationId { get; set; }
        [StringLength(15)]
        [CustomPostcode]
        public string? PostCode { get; set; }
        [StringLength(100)]
        public string? City { get; set; }
    }
}
