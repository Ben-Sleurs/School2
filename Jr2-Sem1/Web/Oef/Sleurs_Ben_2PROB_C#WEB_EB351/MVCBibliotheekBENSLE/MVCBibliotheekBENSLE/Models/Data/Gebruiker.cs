using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MVCBibliotheekBENSLE.Models.Data
{
    public class Gebruiker
    {
        public int? GebruikerId { get; set; }
        [Required]
        public string? Voornaam { get; set; }
        [Required]
        public string? Achternaam { get; set; }
        [Required]
        public string? IdentityUserId { get; set; }
        public ICollection<Reservatie> Reservaties { get; set; }
        public string FullName()
        {
            return Voornaam + " " + Achternaam;
        }
    }
}
