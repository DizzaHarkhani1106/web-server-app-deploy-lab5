using DH_VehicleInventory.Domain.VehicleAggregate;
using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using DH_VehicleInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Infrastructure.Repositories
{
    public class DH_VehicleRepository : IVehicleRepository
    {
        private readonly DH_InventoryDbContext _context;

        public DH_VehicleRepository(DH_InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Vehicle Add(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            _context.DH_Vehicles.Add(vehicle);
            _context.SaveChanges();

            return vehicle;
        }

        public void Update(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            _context.DH_Vehicles.Update(vehicle);
            _context.SaveChanges();
        }

        public void Delete(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            _context.DH_Vehicles.Remove(vehicle);
            _context.SaveChanges();
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Vehicle ID must be positive", nameof(id));

            return await _context.DH_Vehicles.FindAsync(id);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.DH_Vehicles.ToListAsync();
        }
    }
}