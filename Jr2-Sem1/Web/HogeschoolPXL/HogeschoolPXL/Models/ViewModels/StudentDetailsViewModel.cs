using HogeschoolPXL.Models.Data;

namespace HogeschoolPXL.Models.ViewModels
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public List<Inschrijving> Inschrijvingen { get; set; }
    }
}
