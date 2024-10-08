using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace BeverageLib;


public class BeverageContext : DbContext
{
    public DbSet<Beverage> Beverages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;database=beverage_db;user=root;password=yourpassword",
            new MySqlServerVersion(new Version(8, 0, 25)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Beverage model if needed
        modelBuilder.Entity<Beverage>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Brand).IsRequired().HasMaxLength(50);
            entity.Property(b => b.Description).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Price).HasColumnType("decimal(18,2)");
        });
    }
}
