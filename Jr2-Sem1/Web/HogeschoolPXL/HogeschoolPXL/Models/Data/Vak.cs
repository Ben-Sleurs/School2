    namespace HogeschoolPXL.Models.Data
{
    public class Vak
    {
        public int VakId { get; set; }
        public string? VakNaam { get; set; }
        public int? Studiepunten { get; set; }
        public int? HandboekId { get; set; }
        public Handboek? Handboek { get; set; }
        public ICollection<VakLector>? VakLectors { get; set; }

    }
}
