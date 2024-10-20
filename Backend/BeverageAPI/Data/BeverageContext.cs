namespace BeverageAPI.Data;

public class BeverageContext(DbContextOptions<BeverageContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Beverage> Beverages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Beverage Configuration
        modelBuilder.Entity<Beverage>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).ValueGeneratedOnAdd();
            entity.Property(b => b.Name).IsRequired().HasMaxLength(50);
            entity.Property(b => b.Description).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Price).HasColumnType("decimal(18,2)");
        });

        // Customer Configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
        });

        // User Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).ValueGeneratedOnAdd();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.Property(u => u.PasswordHash).IsRequired();

            // User to Customer (One-to-One)
            entity.HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<User>(u => u.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete user when customer is deleted
        });

        // Order Configuration
        modelBuilder.Entity<Order>(static entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id).ValueGeneratedOnAdd();
            entity.Property(o => o.OrderDate).IsRequired();
            entity.Property(o => o.Status)
                .HasDefaultValue(StatusEnum.Pending)
                .HasConversion<int>()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
            entity.Property(o => o.Status)
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

            entity.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

            // Order to Customer (Many-to-One)
            entity.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deleting customers with orders
        });

        // OrderItem Configuration
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.Id);
            entity.Property(oi => oi.Id).ValueGeneratedOnAdd();
            entity.Property(oi => oi.Quantity).IsRequired();
            entity.Property(oi => oi.Price).HasColumnType("decimal(18,2)");

            // OrderItem to Order (Many-to-One)
            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete order items when order is deleted

            // OrderItem to Beverage (Many-to-One)
            entity.HasOne(oi => oi.Beverage)
                .WithMany()
                .HasForeignKey(oi => oi.BeverageId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of beverages in use by orders
        });
    }
}
