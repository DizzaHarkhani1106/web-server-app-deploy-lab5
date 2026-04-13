using Xunit;
using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using System;

namespace DH_VehicleInventory_UnitTests.VehicleAggregate.Entities
{
    public class InventoryTests
    {
        [Fact]
        public void Inventory_CreateWithValidData_Success()
        {
            // Arrange
            int vehicleId = 1;
            var location = Location.Kitchener;  
            var status = VehicleStatus.Available; 

            // Act
            var inventory = new Inventory(vehicleId, location, status);

            // Assert
            Assert.Equal(1, inventory.VehicleId);
            Assert.Equal(location, inventory.Location);
            Assert.Equal(status, inventory.Status);
            Assert.NotEqual(default(DateTime), inventory.LastUpdated);
        }

        [Fact]
        public void Inventory_CreateWithZeroVehicleId_ThrowsException()
        {
            // Arrange
            var location = Location.Kitchener; 
            var status = VehicleStatus.Available; 

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Inventory(0, location, status));
        }

        [Fact]
        public void Inventory_CreateWithNullLocation_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Inventory(1, null, VehicleStatus.Available));  
        }

        [Fact]
        public void Inventory_UpdateStatus_Success()
        {
            // Arrange
            var inventory = new Inventory(
                1,
                Location.Kitchener,  
                VehicleStatus.Available  
            );
            var newStatus = VehicleStatus.Rented;  
            var oldUpdatedTime = inventory.LastUpdated;

            // Act
            System.Threading.Thread.Sleep(10);
            inventory.UpdateStatus(newStatus);

            // Assert
            Assert.Equal(newStatus, inventory.Status);
            Assert.True(inventory.LastUpdated >= oldUpdatedTime);
        }

        [Fact]
        public void Inventory_MoveToLocation_Success()
        {
            // Arrange
            var inventory = new Inventory(
                1,
                Location.Kitchener,  
                VehicleStatus.Available  
            );
            var newLocation = Location.Waterloo; 
            var oldUpdatedTime = inventory.LastUpdated;

            // Act
            System.Threading.Thread.Sleep(10);
            inventory.MoveToLocation(newLocation);

            // Assert
            Assert.Equal(newLocation, inventory.Location);
            Assert.True(inventory.LastUpdated >= oldUpdatedTime);
        }
    }
}
