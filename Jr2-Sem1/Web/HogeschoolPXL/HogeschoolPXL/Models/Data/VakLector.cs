namespace HogeschoolPXL.Models.Data
{
    public class VakLector
    {
        public int VakLectorId { get; set; }
        public int? LectorId { get; set; }
        public int? VakId { get; set; }
        public ICollection<Inschrijving>? Inschrijvingen { get; set; }

        public Lector? Lector { get; set; }
        public Vak? Vak { get; set; }

    }
}
