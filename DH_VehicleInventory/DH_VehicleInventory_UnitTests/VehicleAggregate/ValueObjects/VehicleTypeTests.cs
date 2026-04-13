using Xunit;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;

namespace DH_VehicleInventory_UnitTests.VehicleAggregate.ValueObjects
{
    public class VehicleTypeTests
    {
        [Fact]
        public void VehicleType_CreateWithValidData_Success()
        {
            // Arrange
            int id = 5;
            string name = "Motorcycle";

            // Act
            var type = new VehicleType(id, name);

            // Assert
            Assert.Equal(5, type.Id);
            Assert.Equal("Motorcycle", type.Name);
        }

        [Fact]
        public void VehicleType_CreateWithZeroId_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new VehicleType(0, "Sedan"));
        }

        [Fact]
        public void VehicleType_CreateWithEmptyName_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new VehicleType(1, ""));
        }

        [Theory]
        [InlineData(1, "Sedan")]
        [InlineData(2, "SUV")]
        [InlineData(3, "Truck")]
        public void VehicleType_CreateMultipleTypes_AllValid(int id, string name)
        {
            // Act
            var type = new VehicleType(id, name);

            // Assert
            Assert.Equal(id, type.Id);
            Assert.Equal(name, type.Name);
        }

        [Fact]
        public void VehicleType_PredefinedTypes_WorkCorrectly()
        {
            // Assert
            Assert.Equal(1, VehicleType.Sedan.Id);
            Assert.Equal("Sedan", VehicleType.Sedan.Name);

            Assert.Equal(2, VehicleType.Suv.Id);
            Assert.Equal("SUV", VehicleType.Suv.Name);
        }
    }
}

