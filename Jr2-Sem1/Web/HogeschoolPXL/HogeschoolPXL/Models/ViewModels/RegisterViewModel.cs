namespace HogeschoolPXL.Models.ViewModels
{
    public class RegisterViewModel : LoginViewModel
    {
        public string? Role { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
    }
}
