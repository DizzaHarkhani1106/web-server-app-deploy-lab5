using DH_VehicleInventory.Domain.SeedWork;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using DH_VehicleInventory.Domain.VehicleAggregate.Events;
using DH_VehicleInventory.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace DH_VehicleInventory.Domain.VehicleAggregate.Entities
{
    public class Vehicle : Entity, IAggregateRoot
    {
        private List<Inventory> _inventories = new();

        public VehicleCode VehicleCode { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public VehicleStatus Status { get; private set; }

        public IReadOnlyCollection<Inventory> Inventories => _inventories.AsReadOnly();

        public Vehicle(VehicleCode vehicleCode, VehicleType vehicleType)
        {
            VehicleCode = vehicleCode ?? throw new ArgumentNullException(nameof(vehicleCode), "Vehicle code cannot be null.");
            VehicleType = vehicleType ?? throw new ArgumentNullException(nameof(vehicleType));
            Status = VehicleStatus.Available;

            this.AddDomainEvent(new VehicleCreatedDomainEvent(this));
        }
        private Vehicle() { }

        public void MarkAvailable()
        {
            if (Status == VehicleStatus.Available)
                throw new DomainException("Vehicle is already available.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainException("A reserved vehicle cannot be marked as available without explicit release.");

            Status = VehicleStatus.Available;
            this.AddDomainEvent(new VehicleStatusChangedDomainEvent(Id, Status));
        }

        public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle is already rented.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle is currently reserved and cannot be rented.");

            if (Status == VehicleStatus.Maintenance)
                throw new DomainException("Vehicle is under service and cannot be rented.");

            if (Status != VehicleStatus.Available)
                throw new DomainException($"Vehicle cannot be rented from current status: {Status}.");

            Status = VehicleStatus.Rented;
            this.AddDomainEvent(new VehicleStatusChangedDomainEvent(Id, Status));
        }

        public void MarkReserved()
        {
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle is already reserved.");

            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle is currently rented and cannot be reserved.");

            if (Status == VehicleStatus.Maintenance)
                throw new DomainException("Vehicle is under service and cannot be reserved.");

            if (Status != VehicleStatus.Available)
                throw new DomainException($"Vehicle cannot be reserved from current status: {Status}.");

            Status = VehicleStatus.Reserved;
            this.AddDomainEvent(new VehicleStatusChangedDomainEvent(Id, Status));
        }

        public void MarkServiced()
        {
            if (Status == VehicleStatus.Maintenance)
                throw new DomainException("Vehicle is already under service.");

            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle is currently rented and cannot be sent for service.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle is currently reserved and cannot be sent for service.");

            if (Status != VehicleStatus.Available)
                throw new DomainException($"Vehicle cannot be sent for service from current status: {Status}.");

            Status = VehicleStatus.Maintenance;
            this.AddDomainEvent(new VehicleStatusChangedDomainEvent(Id, Status));
        }

        public void ReleaseReservation()
        {
            if (Status != VehicleStatus.Reserved)
                throw new DomainException("Only reserved vehicles can have their reservation released.");

            Status = VehicleStatus.Available;
            this.AddDomainEvent(new VehicleStatusChangedDomainEvent(Id, Status));
        }

        public void AddInventory(Inventory inventory)
        {
            if (inventory == null)
                throw new ArgumentNullException(nameof(inventory));

            _inventories.Add(inventory);
        }

        public void ChangeType(VehicleType newType)
        {
            VehicleType = newType ?? throw new ArgumentNullException(nameof(newType));
        }
    }
}