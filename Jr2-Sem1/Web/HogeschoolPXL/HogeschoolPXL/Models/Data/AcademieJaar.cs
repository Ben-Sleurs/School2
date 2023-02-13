namespace HogeschoolPXL.Models.Data
{
    public class AcademieJaar
    {
        public int AcademieJaarId { get; set; }
        public DateTime StartDatum { get; set; }
        public ICollection<Inschrijving>? Inschrijvingen { get; set; }

    }
}
