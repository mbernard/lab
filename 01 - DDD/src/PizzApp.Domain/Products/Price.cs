using System;

namespace PizzApp.Domain.Products
{
    public class Price
    {
        public double Value { get; }

        public Price(double value)
        {
            if (value < 0 || value > 10_000)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 0 and 10,000");
            }

            this.Value = value;
        }
    }
}