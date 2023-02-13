namespace MVCTagHelper.Models
{
    public class Medewerker
    {
        public int MedewerkerId { get; set; }
        public int AfdelingId { get; set; }
        public Afdeling Afdeling { get; set; }
        public string Naam { get; set; }
        public string VoorNaam { get; set; }
        public string Email => $"{this.VoorNaam}.{this.Naam}@mail.com";
    }
}
