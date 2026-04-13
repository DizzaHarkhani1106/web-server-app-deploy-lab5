using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Application.Interfaces
{
    public interface DH_IVehicleRepository
    {
        Task<Vehicle> GetByIdAsync(int id);
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(int id);
    }
}
