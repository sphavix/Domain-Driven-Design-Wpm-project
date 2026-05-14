namespace Wpm.SharedKernel
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

        public static implicit operator Weight(decimal value)
        {
            return new Weight(value);
        }

        public static implicit operator decimal(Weight weight)
        {
            return weight.Value;
        }
    }
}
