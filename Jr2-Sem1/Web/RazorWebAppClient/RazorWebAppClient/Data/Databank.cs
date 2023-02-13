namespace RazorWebAppClient.Data
{
    public class Databank
    {
        public static List<Klant> Klanten { get; set; }
        public static List<Location> Locaties { get; set; }
        public static void StartDatabank()
        {
            Klanten = new List<Klant>();
            Locaties = new List<Location>();
            Klanten.Add(new Klant(1, "Klant A",1));
            Klanten.Add(new Klant(2, "Klant B",2));
            Locaties.Add(new Location(1, "3500", "Hasselt"));
            Locaties.Add(new Location(2, "3600", "Genk"));
        }
        public static void AddLocation(string postCode, string city)
        {
            int id = Locaties.Max(x => x.LocationId) + 1;
            Locaties.Add(new Location(id, postCode, city));
        }
        public static void AddKlant(string naam, int locatieId)
        {
            int id = Klanten.Max(x => x.KlantId) + 1;
            Klanten.Add(new Klant(id, naam,locatieId));
        }

    }
}
