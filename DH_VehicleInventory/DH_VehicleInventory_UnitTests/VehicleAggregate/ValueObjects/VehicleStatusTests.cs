using Xunit;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
namespace DH_VehicleInventory_UnitTests.VehicleAggregate.ValueObjects
{
    public class VehicleStatusTests
    {
        [Fact]
        public void VehicleStatus_CreateWithValidData_Success()
        {
            // Arrange
            int id = 5;
            string name = "Sold";

            // Act
            var status = new VehicleStatus(id, name);

            // Assert
            Assert.Equal(5, status.Id);
            Assert.Equal("Sold", status.Name);
        }

        [Fact]
        public void VehicleStatus_CreateWithZeroId_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new VehicleStatus(0, "Available"));
        }

        [Fact]
        public void VehicleStatus_CreateWithEmptyName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new VehicleStatus(1, ""));
        }

        [Theory]
        [InlineData(1, "Available")]
        [InlineData(2, "Reserved")]
        [InlineData(3, "Rented")]
        public void VehicleStatus_CreateMultipleStatuses_AllValid(int id, string name)
        {
            // Act
            var status = new VehicleStatus(id, name);

            // Assert
            Assert.Equal(id, status.Id);
            Assert.Equal(name, status.Name);
        }

        [Fact]
        public void VehicleStatus_PredefinedStatuses_WorkCorrectly()
        {
            // Assert
            Assert.Equal(1, VehicleStatus.Available.Id);
            Assert.Equal("Available", VehicleStatus.Available.Name);

            Assert.Equal(2, VehicleStatus.Reserved.Id);
            Assert.Equal("Reserved", VehicleStatus.Reserved.Name);
        }
    }
}

