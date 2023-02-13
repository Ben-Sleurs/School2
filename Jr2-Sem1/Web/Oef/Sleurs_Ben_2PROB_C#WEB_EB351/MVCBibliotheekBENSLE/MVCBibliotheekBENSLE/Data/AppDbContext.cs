using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCBibliotheekBENSLE.Models.Data;

namespace MVCBibliotheekBENSLE.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Gebruiker> Gebruiker { get; set; }
        public DbSet<VergaderZaal> VergaderZaal { get; set; }
        public DbSet<Reservatie> Reservatie { get; set; }
    }
}
