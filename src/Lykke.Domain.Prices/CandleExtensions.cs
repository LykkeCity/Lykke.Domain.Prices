using System;
using System.Collections.Generic;
using Lykke.Domain.Prices.Contracts;
using Lykke.Domain.Prices.Model;

namespace Lykke.Domain.Prices
{
    public static class CandleExtensions
    {
        /// <summary>
        /// Merges two candles placed in chronological order
        /// </summary>
        /// <param name="prevCandle">Previous candle</param>
        /// <param name="nextCandle">Next candle</param>
        /// <param name="dateTime">
        /// <see cref="IFeedCandle.DateTime"/> of merged candle, if not specified, 
        /// then <see cref="IFeedCandle.DateTime"/> of both candles should be equal, 
        /// and it will be used as merged candle <see cref="IFeedCandle.DateTime"/>
        /// </param>
        /// <returns></returns>
        public static IFeedCandle MergeWith(this IFeedCandle prevCandle, IFeedCandle nextCandle, DateTime? dateTime = null)
        {
            if (prevCandle == null || nextCandle == null)
            {
                return prevCandle ?? nextCandle;
            }

            if (prevCandle.IsBuy != nextCandle.IsBuy)
            {
                throw new InvalidOperationException($"Can't merge buy and sell candles. Source={prevCandle.ToJson()} Update={nextCandle.ToJson()}");
            }

            if (!dateTime.HasValue && prevCandle.DateTime != nextCandle.DateTime)
            {
                throw new InvalidOperationException($"Can't merge candles with different DateTime property. Source={prevCandle.ToJson()} Update={nextCandle.ToJson()}");
            }

            return new FeedCandle
            {
                Open = prevCandle.Open,
                Close = nextCandle.Close,
                High = Math.Max(prevCandle.High, nextCandle.High),
                Low = Math.Min(prevCandle.Low, nextCandle.Low),
                IsBuy = prevCandle.IsBuy,
                DateTime = dateTime ?? prevCandle.DateTime
            };
        }

        /// <summary>
        /// Merges all of candles placed in chronological order
        /// </summary>
        /// <param name="candles">Candles in hronological order</param>
        /// <param name="dateTime">
        /// <see cref="IFeedCandle.DateTime"/> of merged candle, if not specified, 
        /// then <see cref="IFeedCandle.DateTime"/> of all candles should be equal, 
        /// and it will be used as merged candle <see cref="IFeedCandle.DateTime"/>
        /// </param>
        /// <returns>Merged candle, or null, if no candles to merge</returns>
        public static IFeedCandle MergeAll(this IEnumerable<IFeedCandle> candles, DateTime? dateTime = null)
        {
            if (candles == null)
            {
                return null;
            }

            var open = 0d;
            var close = 0d;
            var high = 0d;
            var low = 0d;
            var firstCandleIsBuy = false;
            var firstCandleDateTime = DateTime.MinValue;
            var count = 0;

            using (var enumerator = candles.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var candle = enumerator.Current;

                    if (count == 0)
                    {
                        open = candle.Open;
                        close = candle.Close;
                        high = candle.High;
                        low = candle.Low;
                        firstCandleIsBuy = candle.IsBuy;
                        firstCandleDateTime = candle.DateTime;
                    }
                    else
                    {
                        if (firstCandleIsBuy != candle.IsBuy)
                        {
                            throw new InvalidOperationException($"Can't merge buy and sell candles. Current candle={candle.ToJson()}");
                        }

                        if (!dateTime.HasValue && firstCandleDateTime != candle.DateTime)
                        {
                            throw new InvalidOperationException($"Can't merge candles with different DateTime property. Current candle={candle.ToJson()}");
                        }

                        close = candle.Close;
                        high = Math.Max(high, candle.High);
                        low = Math.Min(low, candle.Low);
                    }

                    count++;
                }
            }

            if (count > 0)
            {
                return new FeedCandle
                {
                    Open = open,
                    Close = close,
                    High = high,
                    Low = low,
                    IsBuy = firstCandleIsBuy,
                    DateTime = dateTime ?? firstCandleDateTime
                };
            }

            return null;
        }

        public static string ToJson(this IFeedCandle candle)
        {
            return (candle != null)
                ? $"{{ open:{candle.Open} close:{candle.Close} high:{candle.High} low:{candle.Low} isBuy:{candle.IsBuy} dateTime:{candle.DateTime:o} }}"
                : string.Empty;
        }
    }
}
