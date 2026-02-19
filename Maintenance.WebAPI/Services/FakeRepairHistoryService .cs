using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Services
{
    public class FakeRepairHistoryService : IRepairHistoryService
    {
            private readonly List<RepairHistoryDto> _repairs = new List<RepairHistoryDto>
            {
            new RepairHistoryDto
            {
                Id = 1,
                VehicleId = 1,
                RepairDate = DateTime.Now.AddDays(-10),
                Description = "Oil change",
                Cost = 89.99m,
                PerformedBy = "Quick Lube"
            },
            new RepairHistoryDto
            {
                Id = 2,
                VehicleId = 1,
                RepairDate = DateTime.Now.AddDays(-40),
                Description = "Brake pad replacement",
                Cost = 350.00m,
                PerformedBy = "Auto Repair Pro"
            }
        };

            public List<RepairHistoryDto> GetByVehicleId(int vehicleId)
            {
                return _repairs.Where(r => r.VehicleId == vehicleId).ToList();
            }

            public List<RepairHistoryDto> GetRepairHistory(int vehicleId)
            {
                return _repairs.Where(r => r.VehicleId == vehicleId).ToList();
            }

            public RepairHistoryDto AddRepair(RepairHistoryDto repair)
            {
                repair.Id = _repairs.Any() ? _repairs.Max(r => r.Id) + 1 : 1;

            
                if (repair.RepairDate == default)
                    repair.RepairDate = DateTime.Now;

                _repairs.Add(repair);
                return repair;
            }
        }
    }









