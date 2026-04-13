using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DH_VehicleInventory.Infrastructure.Data
{
    public class DH_InventoryDbContextFactory
        : IDesignTimeDbContextFactory<DH_InventoryDbContext>
    {
        public DH_InventoryDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../DH_VehicleInventory.WebAPI");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DH_InventoryDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new DH_InventoryDbContext(optionsBuilder.Options);
        }
    }
}