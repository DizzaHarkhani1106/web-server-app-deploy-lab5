using DH_VehicleInventory.Domain.SeedWork;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using System;

namespace DH_VehicleInventory.Domain.VehicleAggregate.Entities
{
    public class Inventory : Entity
    {
        public int VehicleId { get; private set; }
        public Vehicle? Vehicle { get; private set; }
        public Location? Location { get; private set; }
        public VehicleStatus? Status { get; private set; }
        public DateTime LastUpdated { get; private set; }
        private Inventory() { }

        public Inventory(
            int vehicleId,
            Location location,
            VehicleStatus status)
        {
            if (vehicleId <= 0)
                throw new ArgumentException("Vehicle id must be positive");

            VehicleId = vehicleId;
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            LastUpdated = DateTime.UtcNow;
        }

        public void UpdateStatus(VehicleStatus newStatus)
        {
            if (newStatus == null)
                throw new ArgumentNullException(nameof(newStatus));

            Status = newStatus;
            LastUpdated = DateTime.UtcNow;
        }

        public void MoveToLocation(Location newLocation)
        {
            if (newLocation == null)
                throw new ArgumentNullException(nameof(newLocation));

            Location = newLocation;
            LastUpdated = DateTime.UtcNow;
        }
    }
}