using DH_VehicleInventory.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects
{
    public class Location : ValueObject
    {
        public int Id { get; }
        public string Name { get; }

        public Location(int id, string name)
        {
            if (id <= 0)
                throw new ArgumentException("Location id must be positive");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Location name cannot be empty");

            Id = id;
            Name = name;
        }

        public static readonly Location Kitchener = new(1, "Kitchener");
        public static readonly Location Waterloo = new(2, "Waterloo");
        public static readonly Location Cambridge = new(3, "Cambridge");
        public static readonly Location Guelph = new(4, "Guelph");

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }

        public override string ToString() => Name;
    }
}

