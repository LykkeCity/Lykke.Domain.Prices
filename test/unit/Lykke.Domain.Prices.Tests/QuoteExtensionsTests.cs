using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Lykke.Domain.Prices.Contracts;
using Lykke.Domain.Prices.Model;

namespace Lykke.Domain.Prices.Tests
{
    public class QuoteExtensionsTests
    {
        [Fact]
        public void NullReturnedIfNullPassed()
        {
            IEnumerable<Quote> quotes = null;
            var result = quotes.ToCandle(DateTime.Now);
            Assert.Null(result);
        }

        [Fact]
        public void NullReturnedIfEmptyCollectionPassed()
        {
            IEnumerable<Quote> quotes = new Quote[0];
            var result = quotes.ToCandle(DateTime.Now);
            Assert.Null(result);
        }

        [Fact]
        public void SingleQuoteIsHandled()
        {
            DateTime dt = new DateTime(2017, 1, 1);
            IEnumerable<Quote> quotes = new Quote[]
            {
                new Quote() { AssetPair = "btcusd", IsBuy = true, Price = 100, Timestamp = dt },
            };
            ICandle result = quotes.ToCandle(dt);

            Assert.NotNull(result);
            Assert.True(result.Equals(new Candle() { Open = 100, Close = 100, High = 100, Low = 100, IsBuy = true, DateTime = dt }));
        }

        /// <summary>
        /// Two quotes are sorted and merged to candle
        /// </summary>
        [Fact]
        public void QuotesToCandle_NormalScenario1()
        {
            DateTime dt = new DateTime(2017, 1, 1);

            IEnumerable<Quote> quotes = new Quote[]
            {
                new Quote() { AssetPair = "btcusd", IsBuy = true, Price = 100, Timestamp = dt },
                new Quote() { AssetPair = "btcusd", IsBuy = true, Price = 101, Timestamp = dt.AddMinutes(1) }
            };
            ICandle result = quotes.ToCandle(dt);

            Assert.NotNull(result);
            Assert.True(result.Equals(new Candle() { Open = 100, Close = 101, High = 101, Low = 100, IsBuy = true, DateTime = dt }));
        }

        /// <summary>
        /// Two quotes are sorted and merged to candle
        /// </summary>
        [Fact]
        public void QuotesToCandle_NormalScenario2()
        {
            DateTime dt = new DateTime(2017, 1, 1);

            IEnumerable<Quote> quotes = new Quote[]
            {
                new Quote() { AssetPair = "btcusd", IsBuy = false, Price = 100, Timestamp = dt.AddMinutes(1) },
                new Quote() { AssetPair = "btcusd", IsBuy = false, Price = 101, Timestamp = dt }
            };
            ICandle result = quotes.ToCandle(dt);

            Assert.NotNull(result);
            Assert.True(result.Equals(new Candle() { Open = 101, Close = 100, High = 101, Low = 100, IsBuy = false, DateTime = dt }));
        }
    }
}
