namespace HogeschoolPXL.Models.Data
{
    public class Student
    {
        public int StudentId { get; set; }
        public int? GebruikerId { get; set; }
        public Gebruiker? Gebruiker { get; set; }
        public ICollection<Inschrijving>? Inschrijvingen { get; set; }

       
    }
}
