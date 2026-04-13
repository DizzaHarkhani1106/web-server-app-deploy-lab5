using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DH_VehicleInventory.Infrastructure.Data.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("DH_Vehicles");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedOnAdd();

            builder.OwnsOne(v => v.VehicleCode, vc =>
            {
                vc.Property(c => c.Code)
                    .HasColumnName("VehicleCode")
                    .HasMaxLength(20)
                    .IsRequired();
            });

            builder.OwnsOne(v => v.VehicleType, vt =>
            {
                vt.Property(t => t.Id)
                    .HasColumnName("VehicleTypeId");
                vt.Property(t => t.Name)
                    .HasColumnName("VehicleTypeName")
                    .HasMaxLength(50);
            });

            builder.OwnsOne(v => v.Status, vs =>
            {
                vs.Property(s => s.Id)
                    .HasColumnName("StatusId");
                vs.Property(s => s.Name)
                    .HasColumnName("StatusName")
                    .HasMaxLength(30);
            });

            builder.HasMany(v => v.Inventories)
                .WithOne(i => i.Vehicle)
                .HasForeignKey(i => i.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}