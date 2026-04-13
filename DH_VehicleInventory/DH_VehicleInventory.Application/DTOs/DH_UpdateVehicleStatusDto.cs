using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;

namespace DH_VehicleInventory.Application.DTOs
{
    public class DH_UpdateVehicleStatusDto
    {
        public int VehicleId { get; set; }
        public int StatusId { get; set; }
        public VehicleStatus? GetVehicleStatus()
        {
            return StatusId switch
            {
                1 => VehicleStatus.Available,
                2 => VehicleStatus.Rented,
                3 => VehicleStatus.Reserved,
                4 => VehicleStatus.Maintenance,
                _ => null
            };
        }
    }
}