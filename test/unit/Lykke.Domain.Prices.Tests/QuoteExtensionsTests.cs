﻿using System;
using System.Collections.Generic;
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
            IReadOnlyCollection<Quote> quotes = null;
            var result = quotes.ToCandle(DateTime.Now);
            Assert.Null(result);
        }

        [Fact]
        public void NullReturnedIfEmptyCollectionPassed()
        {
            IReadOnlyCollection<Quote> quotes = new Quote[0];
            var result = quotes.ToCandle(DateTime.Now);
            Assert.Null(result);
        }

        [Fact]
        public void SingleQuoteIsHandled()
        {
            DateTime dt = new DateTime(2017, 1, 1);
            IReadOnlyCollection<Quote> quotes = new Quote[]
            {
                new Quote() { AssetPair = "btcusd", IsBuy = true, Price = 100, Timestamp = dt },
            };
            IFeedCandle result = quotes.ToCandle(dt);

            Assert.NotNull(result);
            Assert.True(result.IsEqual(new FeedCandle() { Open = 100, Close = 100, High = 100, Low = 100, IsBuy = true, DateTime = dt }));
        }

        /// <summary>
        /// Two quotes are sorted and merged to candle
        /// </summary>
        [Fact]
        public void QuotesToCandle_NormalScenario1()
        {
            DateTime dt = new DateTime(2017, 1, 1);

            IReadOnlyCollection<Quote> quotes = new Quote[]
            {
                new Quote() { AssetPair = "btcusd", IsBuy = true, Price = 100, Timestamp = dt },
                new Quote() { AssetPair = "btcusd", IsBuy = true, Price = 101, Timestamp = dt.AddMinutes(1) }
            };
            IFeedCandle result = quotes.ToCandle(dt);

            Assert.NotNull(result);
            Assert.True(result.IsEqual(new FeedCandle() { Open = 100, Close = 101, High = 101, Low = 100, IsBuy = true, DateTime = dt }));
        }

        /// <summary>
        /// Two quotes are sorted and merged to candle
        /// </summary>
        [Fact]
        public void QuotesToCandle_NormalScenario2()
        {
            DateTime dt = new DateTime(2017, 1, 1);

            IReadOnlyCollection<Quote> quotes = new Quote[]
            {
                new Quote() { AssetPair = "btcusd", IsBuy = false, Price = 100, Timestamp = dt.AddMinutes(1) },
                new Quote() { AssetPair = "btcusd", IsBuy = false, Price = 101, Timestamp = dt }
            };
            IFeedCandle result = quotes.ToCandle(dt);

            Assert.NotNull(result);
            Assert.True(result.IsEqual(new FeedCandle() { Open = 101, Close = 100, High = 101, Low = 100, IsBuy = false, DateTime = dt }));
        }
    }

    internal static class CandleExtensions
    {
        public static bool IsEqual(this IFeedCandle candle, IFeedCandle other)
        {
            if (other != null && candle != null)
            {
                return candle.DateTime == other.DateTime
                    && candle.Open == other.Open
                    && candle.Close == other.Close
                    && candle.High == other.High
                    && candle.Low == other.Low
                    && candle.IsBuy == other.IsBuy;
            }
            return false;
        }
    }
}
