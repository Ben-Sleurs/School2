using MVCTagHelper.Data;
using MVCTagHelper.Models;

namespace MVCTagHelper.ViewModels
{
    public class MedewerkerCard
    {
        public MedewerkerCard(TagHelperDbContext context,Medewerker medewerker)
        {
            this.MedewerkerId=medewerker.MedewerkerId;
            this.MedewerkerNaam=$"{medewerker.VoorNaam} {medewerker.Naam}";
            this.AfdelingNaam = medewerker.Afdeling.AfdelingNaam;
        }
        public int MedewerkerId { get; set; }
        public string MedewerkerNaam { get; set; }
        public string AfdelingNaam { get; set; }
    }
}
