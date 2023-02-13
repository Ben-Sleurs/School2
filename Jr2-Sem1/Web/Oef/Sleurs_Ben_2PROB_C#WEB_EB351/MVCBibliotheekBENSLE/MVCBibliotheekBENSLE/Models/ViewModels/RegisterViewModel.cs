using System.ComponentModel.DataAnnotations;

namespace MVCBibliotheekBENSLE.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Achternaam { get; set; }
        [Required]
        public string GebruikersNaam { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Wachtwoord { get; set; }
    }
}
