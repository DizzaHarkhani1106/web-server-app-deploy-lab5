using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;

namespace DH_VehicleInventory.Application.DTOs
{
    public class DH_VehicleDto
    {
      
        public int Id { get; set; }
        public string VehicleCode { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public List<InventoryLocationDto> Inventories { get; set; } = new();
    }
    public class InventoryLocationDto
    {
        public int Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
    }
}