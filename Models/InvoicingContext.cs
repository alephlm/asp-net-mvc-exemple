using Microsoft.EntityFrameworkCore;

namespace Invoicing.Models
{
    public class InvoicingContext : DbContext
    {
        public InvoicingContext(DbContextOptions<InvoicingContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Parked> Parkeds { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Parkeds)
                .WithOne(p => p.Customer);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Invoices)
                .WithOne(p => p.Customer);
        }
    }
}