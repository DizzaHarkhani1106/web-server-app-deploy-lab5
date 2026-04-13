using DH_VehicleInventory.Application.DTOs;
using DH_VehicleInventory.Domain.VehicleAggregate;
using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Application.Services
{
    public class DH_VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public DH_VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        }
        public async Task<DH_VehicleDto?> GetVehicleAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Vehicle ID must be positive", nameof(id));

            try
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(id);
                if (vehicle == null)
                    return null;

                return MapVehicleToDto(vehicle);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving vehicle with ID {id}", ex);
            }
        }
        public async Task<IEnumerable<DH_VehicleDto>> GetAllVehiclesAsync()
        {
            try
            {
                var vehicles = await _vehicleRepository.GetAllAsync();
                return vehicles.Select(MapVehicleToDto).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving all vehicles", ex);
            }
        }
        public DH_VehicleDto CreateVehicle(DH_CreateVehicleDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.VehicleCode))
                throw new ArgumentException("Vehicle code is required", nameof(dto.VehicleCode));

            try
            {
                var vehicleType = dto.GetVehicleType();
                if (vehicleType == null)
                    throw new ArgumentException("Invalid vehicle type ID", nameof(dto.VehicleTypeId));

                var vehicleCode = new VehicleCode(dto.VehicleCode);
                var vehicle = new Vehicle(vehicleCode, vehicleType);
                var createdVehicle = _vehicleRepository.Add(vehicle);

                return MapVehicleToDto(createdVehicle);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error creating vehicle with code {dto.VehicleCode}", ex);
            }
        }
        public async Task<bool> UpdateVehicleStatus(int vehicleId, DH_UpdateVehicleStatusDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (vehicleId <= 0)
                throw new ArgumentException("Vehicle ID must be positive", nameof(vehicleId));

            try
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
                if (vehicle == null)
                    return false;

                var newStatus = dto.GetVehicleStatus();
                if (newStatus == null)
                    throw new ArgumentException("Invalid status ID", nameof(dto.StatusId));
                switch (newStatus)
                {
                    case var s when s == VehicleStatus.Available:
                        vehicle.MarkAvailable();
                        break;
                    case var s when s == VehicleStatus.Rented:
                        vehicle.MarkRented();
                        break;
                    case var s when s == VehicleStatus.Reserved:
                        vehicle.MarkReserved();
                        break;
                    case var s when s == VehicleStatus.Maintenance:
                        vehicle.MarkServiced();
                        break;
                }

                _vehicleRepository.Update(vehicle);

                return true;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error updating vehicle status for ID {vehicleId}", ex);
            }
        }
        public async Task<bool> DeleteVehicleAsync(int vehicleId)
        {
            if (vehicleId <= 0)
                throw new ArgumentException("Vehicle ID must be positive", nameof(vehicleId));

            try
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
                if (vehicle == null)
                    return false;

                _vehicleRepository.Delete(vehicle);

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting vehicle with ID {vehicleId}", ex);
            }
        }
        private DH_VehicleDto MapVehicleToDto(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            return new DH_VehicleDto
            {
                Id = vehicle.Id,
                VehicleCode = vehicle.VehicleCode?.ToString() ?? string.Empty,
                VehicleType = vehicle.VehicleType?.Name ?? string.Empty,
                Status = vehicle.Status?.Name ?? string.Empty,
                Inventories = vehicle.Inventories
                    .Select(inv => new InventoryLocationDto
                    {
                        Id = inv.Id,
                        Location = inv.Location?.Name ?? "Unknown",
                        Status = inv.Status?.Name ?? "Unknown",
                        LastUpdated = inv.LastUpdated
                    })
                    .ToList()
            };
        }
    }
}