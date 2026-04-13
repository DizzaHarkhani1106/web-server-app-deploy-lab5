using DH_VehicleInventory.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects
{
    public class VehicleCode : ValueObject
    {
        public string Code { get; }

        public VehicleCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            Code = code;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        public override string ToString() => Code;
    }
}

