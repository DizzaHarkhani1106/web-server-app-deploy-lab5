using DH_VehicleInventory.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects
{
    public class VehicleType : ValueObject
    {
        public int Id { get; }
        public string Name { get; }

        public VehicleType(int id, string name)
        {
            if (id <= 0)
                throw new ArgumentException("Vehicle type id must be positive");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Vehicle type name cannot be empty");

            Id = id;
            Name = name;
        }

        public static readonly VehicleType Sedan = new(1, "Sedan");
        public static readonly VehicleType Suv = new(2, "SUV");
        public static readonly VehicleType Truck = new(3, "Truck");
        public static readonly VehicleType Van = new(4, "Van");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString() => Name;
    }
}

