using Microsoft.EntityFrameworkCore;

namespace Invoicing.Models
{
    public class ParkedContext : DbContext
    {
        public ParkedContext(DbContextOptions<ParkedContext> options)
            : base(options)
        {
        }

        public DbSet<ParkedContext> ParkedItems { get; set; }

    }
}