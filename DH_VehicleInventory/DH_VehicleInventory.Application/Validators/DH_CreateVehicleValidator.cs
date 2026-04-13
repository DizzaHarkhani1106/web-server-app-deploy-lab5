using FluentValidation;
using DH_VehicleInventory.Application.DTOs;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using System.Linq;

namespace DH_VehicleInventory.Application.Validators
{
    public class DH_CreateVehicleValidator : AbstractValidator<DH_CreateVehicleDto>
    {
        public DH_CreateVehicleValidator()
        {
          
            RuleFor(dto => dto.VehicleCode)
                .NotEmpty()
                .WithMessage("Vehicle code is required.")
                .MinimumLength(1)
                .WithMessage("Vehicle code must have at least 1 character.")
                .MaximumLength(50)
                .WithMessage("Vehicle code cannot exceed 50 characters.")
                .Matches(@"^[A-Z0-9]+$")
                .WithMessage("Vehicle code must contain only uppercase letters and numbers.");

        
            RuleFor(dto => dto.VehicleTypeId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Vehicle type ID must be at least 1.")
                .LessThanOrEqualTo(4)
                .WithMessage("Vehicle type ID must not exceed 4.")
                .Must(vt => IsValidVehicleTypeId(vt))
                .WithMessage("Vehicle type ID must be 1 (Sedan), 2 (SUV), 3 (Truck), or 4 (Van).");
        }

        private bool IsValidVehicleTypeId(int vehicleTypeId)
        {
            var validTypes = new[] { 1, 2, 3, 4 };
            return validTypes.Contains(vehicleTypeId);
        }
    }
}