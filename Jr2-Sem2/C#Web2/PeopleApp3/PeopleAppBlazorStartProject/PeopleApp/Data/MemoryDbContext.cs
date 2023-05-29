using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleApp.Models;

namespace PeopleApp.Data;

public class MemoryDbContext : IdentityDbContext
{
    public MemoryDbContext(DbContextOptions<MemoryDbContext> opts) : base(opts)
    {
        
    }
    public DbSet<Person> People { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Location> Locations { get; set; } 
}