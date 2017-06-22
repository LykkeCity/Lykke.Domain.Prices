using System;
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
        /// <returns></returns>
        public static IFeedCandle MergeWith(this IFeedCandle prevCandle, IFeedCandle nextCandle)
        {
            if (prevCandle == null || nextCandle == null)
            {
                return prevCandle ?? nextCandle;
            }

            if (prevCandle.IsBuy != nextCandle.IsBuy)
            {
                throw new InvalidOperationException($"Can't merge buy and sell candles. Source={prevCandle.ToJson()} Update={nextCandle.ToJson()}");
            }
            if (prevCandle.DateTime != nextCandle.DateTime)
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
                DateTime = prevCandle.DateTime
            };
        }

        public static string ToJson(this IFeedCandle candle)
        {
            return (candle != null)
                ? $"{{ open:{candle.Open} close:{candle.Close} high:{candle.High} low:{candle.Low} isBuy:{candle.IsBuy} dateTime:{candle.DateTime:o} }}"
                : string.Empty;
        }
    }
}
