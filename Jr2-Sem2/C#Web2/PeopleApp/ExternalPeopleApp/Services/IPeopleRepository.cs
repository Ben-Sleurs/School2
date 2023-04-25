using ExternalPeopleApp.Data;
using ExternalPeopleApp.Models;

namespace ExternalPeopleApp.Services;

public interface IPeopleRepository
{
    IEnumerable<Person> GetAll();
    IEnumerable<Person> GetById();
    void Add(PersonEditModel person);
    void Update(PersonEditModel person);
    void Delete(Person person);
}