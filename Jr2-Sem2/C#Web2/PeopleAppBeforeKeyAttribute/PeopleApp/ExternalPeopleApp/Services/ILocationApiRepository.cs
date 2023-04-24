using ExternalPeopleApp.Data;

namespace ExternalPeopleApp.Services
{
    public interface ILocationApiRepository
    {
        IEnumerable<Location> GetAll();
    }
}
