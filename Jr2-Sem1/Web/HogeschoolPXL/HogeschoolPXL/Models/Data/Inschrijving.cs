namespace HogeschoolPXL.Models.Data
{
    public class Inschrijving
    {
        public int InschrijvingId { get; set; }
        public int? StudentId { get; set; }
        public int? VakLectorId { get; set; }
        public int? AcademieJaarId { get; set; }
        public Student? Student { get; set; }
        public VakLector? VakLector { get; set; }
        public AcademieJaar? AcademieJaar { get; set; }

    }
}
