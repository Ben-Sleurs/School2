using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCGroentenEnFruit.Models.Data;
using MVCGroentenEnFruit.Models.ViewModels;

namespace MVCGroentenEnFruit.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Artikel>? Artikels { get; set; }
        public DbSet<AankoopOrder>? AankoopOrders { get; set; }
        public DbSet<VerkoopOrder>? VerkoopOrders { get; set; }
        public DbSet<MVCGroentenEnFruit.Models.ViewModels.StockViewModel> StockViewModel { get; set; }
    }
}
