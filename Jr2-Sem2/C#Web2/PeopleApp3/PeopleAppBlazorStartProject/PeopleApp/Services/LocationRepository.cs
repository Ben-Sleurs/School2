using PeopleApp.Data;
using PeopleApp.Models;

namespace PeopleApp.Services
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MemoryDbContext _context;
        public LocationRepository(MemoryDbContext context)
        {
            _context = context;
        }
        public void AddLocation(Location l)
        {
            _context.Locations.Add(l);
            _context.SaveChanges();
        }

        public IEnumerable<Location> GetLocations()
        {
            return _context.Locations;
        }
    }
}
