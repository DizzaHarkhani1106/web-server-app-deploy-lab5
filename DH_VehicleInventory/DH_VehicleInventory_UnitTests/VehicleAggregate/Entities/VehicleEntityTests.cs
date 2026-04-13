using Xunit;
using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using DH_VehicleInventory.Domain.Exceptions;

namespace DH_VehicleInventory_UnitTests.VehicleAggregate.Entities
{
    public class VehicleEntityTests
    {
        [Fact]
        public void Vehicle_CreateWithValidData_Success()
        {
            // Arrange
            var code = new VehicleCode("VH001");
            var type = new VehicleType(1, "Sedan");

            // Act
            var vehicle = new Vehicle(code, type);

            // Assert
            Assert.NotNull(vehicle);
            Assert.Equal(code, vehicle.VehicleCode);
            Assert.Equal(type, vehicle.VehicleType);
            Assert.Equal(VehicleStatus.Available, vehicle.Status);
        }

        [Fact]
        public void Vehicle_CreateWithNullCode_ThrowsException()
        {
            // Arrange
            var type = new VehicleType(1, "Sedan");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Vehicle(null, type));
        }

        [Fact]
        public void Vehicle_MarkRented_Success()
        {
            // Arrange
            var vehicle = new Vehicle(
                new VehicleCode("VH001"),
                new VehicleType(1, "Sedan")
            );

            // Act
            vehicle.MarkRented();

            // Assert
            Assert.Equal(VehicleStatus.Rented, vehicle.Status);
        }

        [Fact]
        public void Vehicle_MarkReserved_Success()
        {
            // Arrange
            var vehicle = new Vehicle(
                new VehicleCode("VH001"),
                new VehicleType(1, "Sedan")
            );

            // Act
            vehicle.MarkReserved();

            // Assert
            Assert.Equal(VehicleStatus.Reserved, vehicle.Status);
        }

        [Fact]
        public void Vehicle_MarkRentedWhenReserved_ThrowsException()
        {
            // Arrange
            var vehicle = new Vehicle(
                new VehicleCode("VH001"),
                new VehicleType(1, "Sedan")
            );
            vehicle.MarkReserved();

            // Act & Assert
            Assert.Throws<DomainException>(() => vehicle.MarkRented());
        }
    }
}

