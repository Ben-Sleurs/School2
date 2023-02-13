namespace HogeschoolPXL.Models.Data
{
    public class Lector
    {
        public int LectorId { get; set; }
        public int? GebruikerId { get; set; }
        public Gebruiker? Gebruiker { get; set; }
        public ICollection<VakLector>? VakLectors { get; set; }
    }
}
