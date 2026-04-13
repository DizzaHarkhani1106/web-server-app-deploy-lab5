using DH_VehicleInventory.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects
{
    public class VehicleStatus : ValueObject
    {
        public int Id { get; }
        public string Name { get; }

        public VehicleStatus(int id, string name)
        {
            if (id <= 0)
                throw new ArgumentException("Vehicle status id must be positive");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Vehicle status name cannot be empty");

            Id = id;
            Name = name;
        }

        public static readonly VehicleStatus Available = new(1, "Available");
        public static readonly VehicleStatus Reserved = new(2, "Reserved");
        public static readonly VehicleStatus Rented = new(3, "Rented");
        public static readonly VehicleStatus Maintenance = new(4, "Maintenance");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString() => Name;
    }
}

