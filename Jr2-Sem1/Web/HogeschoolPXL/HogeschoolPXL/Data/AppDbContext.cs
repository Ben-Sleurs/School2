using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HogeschoolPXL.Models.Data;

namespace HogeschoolPXL.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Gebruiker> Gebruiker { get; set; }
        public DbSet<HogeschoolPXL.Models.Data.Student> Student { get; set; }
        public DbSet<HogeschoolPXL.Models.Data.Vak> Vak { get; set; }
        public DbSet<HogeschoolPXL.Models.Data.AcademieJaar> AcademieJaar { get; set; }
        public DbSet<HogeschoolPXL.Models.Data.Handboek> Handboek { get; set; }
        public DbSet<HogeschoolPXL.Models.Data.Inschrijving> Inschrijving { get; set; }
        public DbSet<HogeschoolPXL.Models.Data.Lector> Lector { get; set; }
        public DbSet<HogeschoolPXL.Models.Data.VakLector> VakLector { get; set; }
    }
}
