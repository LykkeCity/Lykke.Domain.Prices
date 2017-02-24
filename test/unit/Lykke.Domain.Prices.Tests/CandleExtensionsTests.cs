using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Lykke.Domain.Prices.Contracts;
using Lykke.Domain.Prices.Model;

namespace Lykke.Domain.Prices.Tests
{
    public class CandleExtensionsTests
    {
        [Fact]
        public void PriceValuesAreMerged()
        {
            DateTime dt = new DateTime(2017, 1, 1);

            // Case 1: Update values are taken
            //
            var target = new FeedCandle() { Open = 2, Close = 3, High = 4, Low = 1, IsBuy = true, DateTime = dt };
            var update = new FeedCandle() { Open = 3, Close = 2, High = 5, Low = 0.1, IsBuy = true, DateTime = dt };

            IFeedCandle merged = target.MergeWith(update);
            Assert.Equal(update.Open, merged.Open);
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
    }
}
