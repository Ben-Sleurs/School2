
using MVCTagHelper.Models;

namespace MVCTagHelper.Data.DefaultData
{
    public class SeedData
    {
        public static void Populate(WebApplication app)
        {
            using (var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<TagHelperDbContext>())
            {
                //context.Database.ens
                if (!context.Landen.Any())
                {
                    var be = new Land { LandCode = "BE", LandNaam = "België" };
                    var nl = new Land { LandCode = "NL", LandNaam = "Nederland" };
                    var d = new Land { LandCode = "D", LandNaam = "Duitsland" };
                    context.Landen.AddRange(be, nl);
                    var brussel = new Locatie { Stad = "Brussel", Land = be };
                    var hasselt = new Locatie { Stad = "Hasselt", Land = be };
                    var amsterdam = new Locatie { Stad = "AmsterDam", Land = nl };
                    context.Locaties.AddRange(brussel, hasselt, amsterdam);
                    var aankoop = new Afdeling { AfdelingNaam = "Aankoop", Locatie = brussel };
                    var verkoop = new Afdeling { AfdelingNaam = "Verkoop", Locatie = hasselt };
                    var onderzoek = new Afdeling { AfdelingNaam = "Onderzoek", Locatie = amsterdam };
                    context.Afdelingen.AddRange(aankoop, verkoop, onderzoek);
                    var kristof = new Medewerker { Afdeling=aankoop, Naam = "Evaert", VoorNaam = "Kristof" };
                    var bobin = new Medewerker { Afdeling = verkoop, Naam = "Boger", VoorNaam = "Bobin" };
                    var ben = new Medewerker { Afdeling = onderzoek, Naam = "Sleurs", VoorNaam = "Ben" };
                    context.Medewerker.AddRange(kristof, bobin, ben);
                    context.SaveChanges();
                }
            }
        }
    }
}
