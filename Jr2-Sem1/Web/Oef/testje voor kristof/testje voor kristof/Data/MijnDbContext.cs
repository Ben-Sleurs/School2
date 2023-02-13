using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using testje_voor_kristof.Models;

namespace testje_voor_kristof.Data
{
    public class MijnDbContext:DbContext
    {
        public MijnDbContext(DbContextOptions<MijnDbContext> options):base(options)
        {

        }
        public DbSet<HetBesteModel> HetBesteModels { get; set; }
    }
}
