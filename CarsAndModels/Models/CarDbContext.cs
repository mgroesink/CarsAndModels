using CarsAndModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

public class CarDbContext : DbContext
{
    public DbSet<CarBrand> CarBrands { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<VehicleModelYear> VehicleModels { get; set; }
    public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Hier configureer je de database provider (bijv. SQL Server, SQLite, etc.) en de connection string
        // optionsBuilder.UseSqlServer("Server=.;Initial Catalog=CardAndModels;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
    }
}
