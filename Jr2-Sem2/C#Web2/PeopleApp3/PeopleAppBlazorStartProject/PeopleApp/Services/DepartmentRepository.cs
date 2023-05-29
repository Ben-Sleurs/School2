using PeopleApp.Data;
using PeopleApp.Models;

namespace PeopleApp.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MemoryDbContext _context;
        public DepartmentRepository(MemoryDbContext context)
        {
            _context = context;
        }        
        public void AddDepartment(Department d)
        {
            _context.Departments.Add(d);
            _context.SaveChanges();
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments;
        }
    }
}
