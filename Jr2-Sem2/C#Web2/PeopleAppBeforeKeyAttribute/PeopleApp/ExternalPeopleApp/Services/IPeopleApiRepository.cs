using ExternalPeopleApp.Data;
using ExternalPeopleApp.Models;

namespace ExternalPeopleApp.Services
{
    public interface IPeopleApiRepository
    {
        IEnumerable<Person> GetAll();
        void Add(PersonEditModel person);
    }
}
