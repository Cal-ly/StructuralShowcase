namespace BeverageAPI.Models;

public class BeverageContext(DbContextOptions<BeverageContext> options) : DbContext(options)
{
    public DbSet<Beverage> Beverages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Beverage>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Brand).IsRequired().HasMaxLength(50);
            entity.Property(b => b.Description).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Price).HasColumnType("decimal(18,2)");
        });
    }
}
