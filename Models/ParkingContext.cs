using Microsoft.EntityFrameworkCore;

namespace Invoicing.Models
{
    public class ParkingContext : DbContext
    {
        public ParkingContext(DbContextOptions<ParkingContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingContext> ParkingItems { get; set; }

    }
}