using System;
using Lykke.Domain.Prices.Contracts;
using Xunit;
using Lykke.Domain.Prices.Model;

namespace Lykke.Domain.Prices.Tests
{
    public class CandleExtensionsTests
    {
        [Fact]
        public void PriceValuesAreMerged()
        {
            var dt = new DateTime(2017, 1, 1);

            // Case 1: Update values are taken
            //
            var target = new FeedCandle() { Open = 2, Close = 3, High = 4, Low = 1, IsBuy = true, DateTime = dt };
            var update = new FeedCandle() { Open = 3, Close = 2, High = 5, Low = 0.1, IsBuy = true, DateTime = dt };

            var merged = target.MergeWith(update);

            Assert.Equal(target.Open, merged.Open);
            Assert.Equal(update.Close, merged.Close);
            Assert.Equal(update.High, merged.High);
            Assert.Equal(update.Low, merged.Low);
            Assert.True(merged.IsBuy);
            Assert.Equal(merged.DateTime, dt);

            // Case 2: Target values are preserved
            //
            update = new FeedCandle() { Open = 3, Close = 2, High = 3, Low = 2, IsBuy = true, DateTime = dt };

            merged = target.MergeWith(update);

            Assert.Equal(target.High, merged.High);
            Assert.Equal(target.Low, merged.Low);
        }

        [Fact]
        public void NewDateIsUsedAndCandlesMayHaveDifferentDates()
        {
            // Arrange
            var newDate = new DateTime(2017, 1, 3);
            var target = new FeedCandle { Open = 2, Close = 3, High = 4, Low = 1, IsBuy = true, DateTime = new DateTime(2017, 1, 1) };
            var update = new FeedCandle { Open = 3, Close = 2, High = 5, Low = 0.1, IsBuy = true, DateTime = new DateTime(2017, 1, 2) };

            // Act
            var merged = target.MergeWith(update, newDate);

            // Assert
            Assert.Equal(merged.DateTime, newDate);
        }

        [Fact]
        public void MergingBuySellIsUnsupported()
        {
            DateTime dt = new DateTime(2017, 1, 1);
            var target = new FeedCandle() { Open = 2, Close = 3, High = 4, Low = 1, IsBuy = true, DateTime = dt };
            var update = new FeedCandle() { Open = 3, Close = 2, High = 5, Low = 0.1, IsBuy = false, DateTime = dt };

            Assert.Throws(typeof(InvalidOperationException), () => target.MergeWith(update));
        }

        [Fact]
        public void MergingCandlesWithDiffTimeIsUnsupported()
        {
            var target = new FeedCandle() { Open = 2, Close = 3, High = 4, Low = 1, IsBuy = true, DateTime = new DateTime(2017, 1, 1) };
            var update = new FeedCandle() { Open = 3, Close = 2, High = 5, Low = 0.1, IsBuy = true, DateTime = new DateTime(2017, 1, 2) };

            Assert.Throws(typeof(InvalidOperationException), () => target.MergeWith(update));
        }

        [Fact]
        public void EmptyCollectionMergedToNull()
        {
            // Arrange
            var candles = new IFeedCandle[0];

            // Act
            var merged = candles.MergeAll();

            // Assert
            Assert.Null(merged);
        }

        [Fact]
        public void SingleElementCollectionMergedToEqualElement()
        {
            // Arrange
            var candles = new IFeedCandle[]
            {
                new FeedCandle {Open = 2, Close = 3, High = 4, Low = 1, IsBuy = true, DateTime = new DateTime(2017, 1, 1)}
            };

            // Act
            var merged = candles.MergeAll();

            // Assert
            Assert.NotNull(merged);
            Assert.Equal(candles[0].Open, merged.Open);
            Assert.Equal(candles[0].Close, merged.Close);
            Assert.Equal(candles[0].High, merged.High);
            Assert.Equal(candles[0].Low, merged.Low);
            Assert.Equal(candles[0].IsBuy, merged.IsBuy);
            Assert.Equal(candles[0].DateTime, merged.DateTime);
        }

        [Fact]
        public void CollectionMergedCorrectly()
        {
            // Arrange
            var candles = new IFeedCandle[]
            {
                new FeedCandle {Open = 2, Close = 3, High = 3, Low = 2, IsBuy = true, DateTime = new DateTime(2017, 1, 1)},
                new FeedCandle {Open = 3, Close = 2, High = 4, Low = 3, IsBuy = true, DateTime = new DateTime(2017, 1, 1)},
                new FeedCandle {Open = 1, Close = 4, High = 2, Low = 1, IsBuy = true, DateTime = new DateTime(2017, 1, 1)},
                new FeedCandle {Open = 4, Close = 1, High = 5, Low = 4, IsBuy = true, DateTime = new DateTime(2017, 1, 1)}
            };

            // Act
            var merged = candles.MergeAll();

            // Assert
            Assert.NotNull(merged);
            Assert.Equal(candles[0].Open, merged.Open);
            Assert.Equal(candles[3].Close, merged.Close);
            Assert.Equal(candles[3].High, merged.High);
            Assert.Equal(candles[2].Low, merged.Low);
            Assert.Equal(candles[0].IsBuy, merged.IsBuy);
            Assert.Equal(candles[0].DateTime, merged.DateTime);
        }

        [Fact]
        public void NewDateIsUsedAndCandlesCollectionMayHaveDifferentDates()
        {
            // Arrange
            var newDate = new DateTime(2017, 1, 5);
            var candles = new IFeedCandle[]
            {
                new FeedCandle {Open = 2, Close = 3, High = 3, Low = 2, IsBuy = true, DateTime = new DateTime(2017, 1, 1)},
                new FeedCandle {Open = 3, Close = 2, High = 4, Low = 3, IsBuy = true, DateTime = new DateTime(2017, 1, 2)},
                new FeedCandle {Open = 1, Close = 4, High = 2, Low = 1, IsBuy = true, DateTime = new DateTime(2017, 1, 3)},
                new FeedCandle {Open = 4, Close = 1, High = 5, Low = 4, IsBuy = true, DateTime = new DateTime(2017, 1, 4)}
            };

            // Act
            var merged = candles.MergeAll(newDate);

            // Assert
            Assert.Equal(merged.DateTime, newDate);
        }

        [Fact]
        public void MergingBuySellCollectionIsUnsupported()
        {
            // Arrange
            var candles = new IFeedCandle[]
            {
                new FeedCandle {Open = 2, Close = 3, High = 3, Low = 2, IsBuy = true, DateTime = new DateTime(2017, 1, 1)},
                new FeedCandle {Open = 3, Close = 2, High = 4, Low = 3, IsBuy = false, DateTime = new DateTime(2017, 1, 1)},
            };

            // Act/Assert
            Assert.Throws(typeof(InvalidOperationException), () => candles.MergeAll());
        }

        [Fact]
        public void MergingCandlesCollectionWithDiffTimeIsUnsupported()
        {
            // Arrange
            var candles = new IFeedCandle[]
            {
                new FeedCandle {Open = 2, Close = 3, High = 3, Low = 2, IsBuy = true, DateTime = new DateTime(2017, 1, 1)},
                new FeedCandle {Open = 3, Close = 2, High = 4, Low = 3, IsBuy = true, DateTime = new DateTime(2017, 1, 2)},
            };

            // Act/Assert
            Assert.Throws(typeof(InvalidOperationException), () => candles.MergeAll());
        }
    }
}
