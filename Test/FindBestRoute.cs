using MateMachine.Core;

namespace Test
{
    public class FindBestRoute
    {
        [Fact]
        public void Sample1()
        {
            var converter = new CurrencyConverter();
            converter.UpdateConfiguration([
                new("USD", "CAD", 1.2),
                new("CAD", "EUR", 1.1),
                new("EUR", "USD", 1.4),
                new("JPY", "USD", 1.11),
            ]);

            Assert.True(converter.Convert("USD", "CAD", 1000).Equals(1200));
        }

        [Fact]
        public void Sample2()
        {
            var converter = new CurrencyConverter();
            converter.UpdateConfiguration([
                new("USD", "CAD", 1.2),
                new("CAD", "EUR", 1.1),
                new("EUR", "USD", 1.4),
                new("JPY", "USD", 1.11),
            ]);

            Assert.True(converter.Convert("JPY", "CAD", 1000).Equals(1332));
        }

        [Fact]
        public void Sample3()
        {
            var converter = new CurrencyConverter();
            converter.UpdateConfiguration([
                new("USD", "CAD", 1.2),
                new("CAD", "EUR", 1.1),
                new("EUR", "USD", 1.4),
                new("JPY", "USD", 1.11),
            ]);

            Assert.True(converter.Convert("JPY", "EUR", 1000).Equals(792.86));
        }
    }
}