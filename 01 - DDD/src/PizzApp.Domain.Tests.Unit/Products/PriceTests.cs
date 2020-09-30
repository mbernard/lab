using System;

using FsCheck;
using FsCheck.Xunit;

using PizzApp.Domain.Products;

namespace PizzApp.Domain.Tests.Unit.Products
{
    public class PriceTests
    {
        [Property(Arbitrary = new[] { typeof(ValidPriceGenerator) })]
        public Property PriceIsInValidRange(double value) => (new Price(value).Value == value).ToProperty();

        [Property(Arbitrary = new[] { typeof(InvalidPriceGenerator) })]
        public Property PriceIsInInvalidRange(double value) =>
            Prop.Throws<ArgumentOutOfRangeException, Price>(new Lazy<Price>(() => new Price(value)));
    }

    public static class ValidPriceGenerator
    {
        public static Arbitrary<double> Generate() => Arb.Default.Float().Filter(x => x >= 0 && x <= 10_000);
    }

    public static class InvalidPriceGenerator
    {
        public static Arbitrary<double> Generate() => Arb.Default.Float().Filter(x => x < 0 || x > 10_000);
    }
}