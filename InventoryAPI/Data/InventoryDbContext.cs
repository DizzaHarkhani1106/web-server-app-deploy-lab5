using InventoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Data
{
    public class InventoryDbContext: DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

            public DbSet<Inventory> Inventories { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Inventory>().HasData(
                    new Inventory { Id = 1, Name = "Toyota Camry", Quantity = 5, Price = 25000 },
                    new Inventory { Id = 2, Name = "Honda Civic", Quantity = 3, Price = 22000 },
                    new Inventory { Id = 3, Name = "Ford Focus", Quantity = 7, Price = 20000 }
                );
            }
        }
    }

