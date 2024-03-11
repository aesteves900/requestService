using Microsoft.EntityFrameworkCore;
using ProductAndRequests.Models;

namespace ProductAndRequests.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable(nameof(Product), schema: "dbo");

            modelBuilder.Entity<Order>().ToTable(nameof(Order), schema: "dbo");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(p => p.TotalPrice)
                .HasPrecision(10, 2);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=OrdersDb;Trusted_Connection=True;TrustServerCertificate=true;Integrated Security=True;Connection Timeout=30");
        }
    }
}