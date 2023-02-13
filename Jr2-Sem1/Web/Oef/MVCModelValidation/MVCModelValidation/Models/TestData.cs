using MVCModelValidation.CustomModelValidation;
using System.ComponentModel.DataAnnotations;

namespace MVCModelValidation.Models
{
    public class TestData
    {
        public int? TestDataId { get; set; }
        [Required]
        [StringLength(10)]
        public string? Tekst{ get; set; }
        [CustomDate]
        public DateTime? Datum { get; set; }
    }
}
