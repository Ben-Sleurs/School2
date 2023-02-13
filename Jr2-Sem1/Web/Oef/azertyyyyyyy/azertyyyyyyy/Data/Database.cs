using azertyyyyyyy.Models;
using Microsoft.EntityFrameworkCore;

namespace azertyyyyyyy.Data
{
    public class Database:DbContext
    {
        public Database(DbContextOptions<Database> options):base(options)
        {

        }
        public DbSet<EenModel> EenModels { get; set; }
    }
}
