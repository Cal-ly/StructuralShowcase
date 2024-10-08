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
            entity.Property(b => b.Id).ValueGeneratedOnAdd();
            entity.Property(b => b.Name).IsRequired().HasMaxLength(50);
            entity.Property(b => b.Description).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Price).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Beverage>().HasData(
            new Beverage { Name = "Faxe Kondi", Description = "Refreshing lemon-lime soda", Price = 15.99m, Size = SizeEnum.MediumCan },
            new Beverage { Name = "Ceres Top", Description = "Classic Danish pilsner beer", Price = 20.50m, Size = SizeEnum.MediumBottle },
            new Beverage { Name = "Royal Pilsner", Description = "Light and crisp pilsner beer", Price = 18.00m, Size = SizeEnum.MediumBottle },
            new Beverage { Name = "Royal Export", Description = "Strong lager with a rich taste", Price = 19.50m, Size = SizeEnum.LargeBottle },
            new Beverage { Name = "Royal Classic", Description = "Smooth, amber lager", Price = 17.50m, Size = SizeEnum.MediumBottle },
            new Beverage { Name = "Royal Økologisk", Description = "Organic pilsner with a mild taste", Price = 22.00m, Size = SizeEnum.SmallBottle },
            new Beverage { Name = "Faxe Kondi Booster", Description = "Energy drink with a lemon-lime flavor", Price = 12.99m, Size = SizeEnum.SmallCan },
            new Beverage { Name = "Egekilde Citrus", Description = "Sparkling water with a hint of citrus", Price = 10.00m, Size = SizeEnum.SmallBottle },
            new Beverage { Name = "Egekilde Elderflower", Description = "Sparkling water with elderflower taste", Price = 10.00m, Size = SizeEnum.SmallBottle },
            new Beverage { Name = "Albani Odense Classic", Description = "Full-bodied dark lager", Price = 18.50m, Size = SizeEnum.MediumBottle },
            new Beverage { Name = "Albani Giraf Beer", Description = "Strong beer with a distinct flavor", Price = 24.00m, Size = SizeEnum.XLargeBottle },
            new Beverage { Name = "Albani Mosaic IPA", Description = "Hoppy IPA with tropical notes", Price = 25.00m, Size = SizeEnum.MediumBottle },
            new Beverage { Name = "Faxe Kondi Free", Description = "Sugar-free version of the classic soda", Price = 15.99m, Size = SizeEnum.SmallCan },
            new Beverage { Name = "Pepsi Max", Description = "Sugar-free cola", Price = 16.50m, Size = SizeEnum.MediumCan },
            new Beverage { Name = "Royal Beer", Description = "Rich, full-bodied lager", Price = 18.99m, Size = SizeEnum.LargeBottle }
        );
    }
}
