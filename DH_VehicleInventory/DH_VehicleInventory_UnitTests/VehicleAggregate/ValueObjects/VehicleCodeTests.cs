using Xunit;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;


namespace DH_VehicleInventory_UnitTests.VehicleAggregate.ValueObjects
{
    public class VehicleCodeTests
    {
        [Fact]
        public void VehicleCode_CreateWithValidCode_Success()
        {
            // Arrange
            string code = "VH001";

            // Act
            var vehicleCode = new VehicleCode(code);

            // Assert
            Assert.Equal("VH001", vehicleCode.Code); 
        }

        [Fact]
        public void VehicleCode_CreateWithNull_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new VehicleCode(null));
        }

        [Theory]
        [InlineData("VH001")]
        [InlineData("VH002")]
        [InlineData("VH003")]
        public void VehicleCode_CreateMultipleCodes_AllValid(string code)
        {
            // Act
            var vehicleCode = new VehicleCode(code);

            // Assert
            Assert.NotNull(vehicleCode);
            Assert.Equal(code, vehicleCode.Code);  
        }
    }
}
