using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DH_VehicleInventory.Infrastructure.Data.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("DH_Inventories");
            builder.HasKey(i => i.Id);

            // Value Object: Location (Owned)
            builder.OwnsOne(i => i.Location, l =>
            {
                l.Property(x => x.Id)
                    .HasColumnName("LocationId");

                l.Property(x => x.Name)
                    .HasColumnName("LocationName")
                    .HasMaxLength(50);
            });

            // Value Object: VehicleStatus (Owned)
            builder.OwnsOne(i => i.Status, s =>
            {
                s.Property(x => x.Id)
                    .HasColumnName("StatusId");

                s.Property(x => x.Name)
                    .HasColumnName("StatusName")
                    .HasMaxLength(30);
            });

            builder.Property(i => i.LastUpdated)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(i => i.Vehicle)
                .WithMany(v => v.Inventories)
                .HasForeignKey(i => i.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}