using Microsoft.EntityFrameworkCore;
using WebAppMVCClientLocationEFCore.Models;

namespace WebAppMVCClientLocationEFCore.Data
{
    public class ClientLocationContext: DbContext
    {
        public ClientLocationContext(DbContextOptions<ClientLocationContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
