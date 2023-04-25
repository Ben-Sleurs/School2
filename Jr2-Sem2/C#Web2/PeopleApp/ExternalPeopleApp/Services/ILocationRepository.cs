using ExternalPeopleApp.Data;

namespace ExternalPeopleApp.Services;

public interface ILocationRepository
{
    IEnumerable<Location> GetAll();
}