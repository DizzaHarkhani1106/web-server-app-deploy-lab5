using DH_VehicleInventory.Domain.SeedWork;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Domain.VehicleAggregate.Events
{
    public class VehicleStatusChangedDomainEvent : IDomainEvent
    {
        public int VehicleId { get; }
        public VehicleStatus NewStatus { get; }
        public DateTime OccurredOn { get; }

        public VehicleStatusChangedDomainEvent(int vehicleId, VehicleStatus newStatus)
        {
            VehicleId = vehicleId;
            NewStatus = newStatus;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
