namespace HogeschoolPXL.Models.Data
{
    public class Handboek
    {
        public int HandboekId { get; set; }
        public string? Titel { get; set; }
        public int? KostPrijs { get; set; }
        public DateTime? UitgifteDatum { get; set; }
        public string? Afbeelding { get; set; }
        public ICollection<Vak>? Vakken { get; set; }
    }
}
