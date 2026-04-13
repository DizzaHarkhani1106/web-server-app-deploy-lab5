using DH_VehicleInventory.Domain.SeedWork;
using DH_VehicleInventory.Domain.VehicleAggregate.Entities;

public interface IVehicleRepository : IRepository<Vehicle>
{
   
    Vehicle Add(Vehicle vehicle);
    void Update(Vehicle vehicle);
    void Delete(Vehicle vehicle);
    Task<Vehicle> GetByIdAsync(int id);
    Task<IEnumerable<Vehicle>> GetAllAsync();
}