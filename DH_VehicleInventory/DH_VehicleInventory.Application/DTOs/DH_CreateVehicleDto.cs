using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;

namespace DH_VehicleInventory.Application.DTOs
{

    public class DH_CreateVehicleDto
    {
        public string VehicleCode { get; set; } = string.Empty;
        public int VehicleTypeId { get; set; }
        public VehicleType? GetVehicleType()
        {
            return VehicleTypeId switch
            {
                1 => VehicleType.Sedan,
                2 => VehicleType.Suv,
                3 => VehicleType.Truck,
                4 => VehicleType.Van,
                _ => null
            };
        }
    }
}