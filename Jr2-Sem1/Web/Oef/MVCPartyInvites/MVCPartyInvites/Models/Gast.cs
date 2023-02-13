using System.ComponentModel.DataAnnotations;

namespace MVCPartyInvites.Models
{
    public class Gast
    {
        [Required]
        public string? Naam { get; set; }
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Vul je telefoonnummer in!")]
        public string? Telefoon { get; set; }
        [Required(ErrorMessage ="Laat weten of je aanwezig zal zijn!")]
        public bool? Aanwezig { get; set; }
    }
}
