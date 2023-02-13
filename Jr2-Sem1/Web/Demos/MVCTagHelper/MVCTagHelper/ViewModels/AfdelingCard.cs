using MVCTagHelper.Data;
using MVCTagHelper.Models;

namespace MVCTagHelper.ViewModels
{
    public class AfdelingCard
    {
        //TagHelperContext _context
        public AfdelingCard(TagHelperDbContext context, Afdeling afdeling)
        {
            //_context = context;
            AfdelingId = afdeling.AfdelingId;
            AfdelingNaam = afdeling.AfdelingNaam;
            Locatie = afdeling.Locatie.Stad;
            Land = afdeling.Locatie.Land.LandNaam;
            LandCode = afdeling.Locatie.Land.LandCode;
            Afdeling = afdeling;

        }
        public Afdeling Afdeling { get; set; }
        public int AfdelingId { get; set; }
        public string AfdelingNaam { get; set; }
        public string Locatie { get; set; }
        public string LandCode { get; set; }
        public string Land { get; set; }
    }
}
