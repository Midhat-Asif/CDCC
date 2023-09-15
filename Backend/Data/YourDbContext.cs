using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class YourDbContext : DbContext
 {
    public DbSet<Project> Projects { get; set; }
    public DbSet<myOrders> MyOrders { get; set; } // Change the DbSet name to match the table name

    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure your entity mappings here if needed
    }
 }

}
