using System;
using System.Collections.Generic;
using System.Text;

namespace Wpm.Management.Domain.ValueObjects
{
    public record Weight
    {
        public decimal Value { get; init; }

        public Weight(decimal value)
        {
            if(value < 0)
            {
                throw new ArgumentException("Weight value is invalid.");
            }
            Value = value;
        }

        public static implicit operator Weight(double value)
        {
            return new Weight(value);
        }
    }
}
