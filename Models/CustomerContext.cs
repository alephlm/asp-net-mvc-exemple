using Microsoft.EntityFrameworkCore;

namespace Invoicing.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerContext> CustomerItems { get; set; }

    }
}