using Microsoft.EntityFrameworkCore;
using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
namespace DH_VehicleInventory.Infrastructure.Data
{
    public class DH_InventoryDbContext: DbContext
    {
        public DH_InventoryDbContext(DbContextOptions<DH_InventoryDbContext> options) : base(options)
        {

        }
        public DbSet<Vehicle> DH_Vehicles { get; set; }
        public DbSet<Inventory> DH_Inventories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DH_InventoryDbContext).Assembly);
        }
    }
}
