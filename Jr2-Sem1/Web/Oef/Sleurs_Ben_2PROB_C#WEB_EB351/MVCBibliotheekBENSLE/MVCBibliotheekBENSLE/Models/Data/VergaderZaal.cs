using MVCBibliotheekBENSLE.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace MVCBibliotheekBENSLE.Models.Data
{
    public class VergaderZaal
    {
        public int? VergaderZaalId { get; set; }
        [Required]
        [CustomVergaderZaalNameValidation]
        public string? Naam { get; set; }
        [Required]
        [CustomVergaderZaalPersonValidation]
        public int? AantalPersonen { get; set; }
        public ICollection<Reservatie>? Reservaties { get; set; }
    }
}
