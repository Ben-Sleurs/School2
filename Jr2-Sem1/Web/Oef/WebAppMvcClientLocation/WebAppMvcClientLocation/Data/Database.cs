using System.Runtime.CompilerServices;
using WebAppMvcClientLocation.Models;

namespace WebAppMvcClientLocation.Data
{
    public static class Database

    {
        static Database()
        {
            Location location1 = new Location();
            location1.LocationId = 1;
            location1.City = "Neerpelt";
            location1.Postcode = "3910;";
            AddLocation(location1);
            Location location2 = new Location();
            location2.LocationId = 2;
            location2.City = "Bocholt";
            location2.Postcode = "3950;";
            AddLocation(location2);


            Client klant1 = new Client();
            klant1.ClientId = 1;
            klant1.ClientName = "Bobin";
            klant1.LocationId = 2;
            AddClient(klant1);
            AddClient(new Client
            {
                ClientId = 2,
                ClientName = "Ben",
                LocationId = 1
            });

        }
        private static List<Client> clients = new List<Client>();
        private static List<Location> locations = new List<Location>();
        public static IEnumerable<Client> Clients => clients;
        public static IEnumerable<Location> Locations => locations;
        public static InsertResult AddClient(Client cs)
        {
            InsertResult ir = new InsertResult();
            try
            {
                clients.Add(cs);
                ir.Succeeded = true;
                return ir;
            }
            catch (Exception ex)
            {
                ir.Errors.Add(ex.Message);
                ir.Succeeded = false;
                return ir;
            }
            
        }
        public static InsertResult AddLocation(Location l)
        {
            InsertResult ir = new InsertResult();
            try
            {
                locations.Add(l);
                ir.Succeeded = true;
                return ir;
            }
            catch (Exception ex)
            {
                ir.Errors.Add(ex.Message);
                ir.Succeeded = false;
                return ir;
            }
        }

    }
}
