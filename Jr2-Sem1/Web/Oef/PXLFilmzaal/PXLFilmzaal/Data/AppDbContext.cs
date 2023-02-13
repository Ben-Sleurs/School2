using Microsoft.EntityFrameworkCore;
using PXLFilmzaal.Models.Data;

namespace PXLFilmzaal.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmImage> FilmImages { get; set; }
    }   
}
