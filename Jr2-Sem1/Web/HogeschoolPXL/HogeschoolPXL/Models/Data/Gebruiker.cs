using HogeschoolPXL.Data.DefaultData;

namespace HogeschoolPXL.Models.Data
{
    public class Gebruiker
    {
        public int? GebruikerId { get; set; }
        public string? Naam { get; set; }
        public string? Voornaam { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public Lector? Lector { get; set; }
        public Student? Student { get; set; }


        public string? FullName()
        {
            return this.Naam + " " + this.Voornaam;
        }

    }
}
