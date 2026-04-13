using DH_VehicleInventory.Domain.SeedWork;
using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Domain.VehicleAggregate.Events
{
    public class VehicleCreatedDomainEvent : IDomainEvent
    {
        public int VehicleId { get; }
        public string VehicleCode { get; }
        public DateTime OccurredOn { get; }

        public VehicleCreatedDomainEvent(Vehicle vehicle)
        {
            VehicleId = vehicle.Id;
            VehicleCode = vehicle.VehicleCode.Code;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
