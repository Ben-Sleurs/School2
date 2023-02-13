using ClassLibBooking.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVCBooking.Data
{
    public class BookingDbContext : IdentityDbContext<Student>
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        { 

        }
        public DbSet<Booking> Bookings { get; set; }
    }
}
