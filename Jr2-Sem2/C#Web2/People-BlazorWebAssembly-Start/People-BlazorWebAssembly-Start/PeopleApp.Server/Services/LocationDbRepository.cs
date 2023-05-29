using System.Collections.Generic;
using System.Linq;
using PeopleApp.Data;
using PeopleApp.Shared.Entities;

namespace PeopleApp.Services
{
    public class LocationDbRepository : ILocationRepository
    {
        private readonly DataContext _context;

        public LocationDbRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetAll()
        {
            return _context.Locations.ToList();
        }
    }
}