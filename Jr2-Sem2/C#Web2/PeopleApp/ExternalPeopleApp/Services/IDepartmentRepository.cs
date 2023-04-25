using ExternalPeopleApp.Data;

namespace ExternalPeopleApp.Services;

public interface IDepartmentRepository
{
    IEnumerable<Department> GetAll();
}