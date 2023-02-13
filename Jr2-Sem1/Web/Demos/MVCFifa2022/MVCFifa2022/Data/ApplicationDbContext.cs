using Microsoft.EntityFrameworkCore;
using MVCFifa2022.Models;

namespace MVCFifa2022.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamPlayer> TeamPlayer { get; set; }
    }
}
