using ExternalPeopleApp.Data;

namespace ExternalPeopleApp.Services
{
    public interface IDepartmentApiRepository
    {
        IEnumerable<Department> GetAll();
    }
}
