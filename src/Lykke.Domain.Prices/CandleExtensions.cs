using System;
using Lykke.Domain.Prices.Contracts;
using Lykke.Domain.Prices.Model;

namespace Lykke.Domain.Prices
{
    public static class CandleExtensions
    {
        public static IFeedCandle MergeWith(this IFeedCandle target, IFeedCandle update)
        {
            if (target == null || update == null)
            {
                return target ?? update;
            }

            if (target.IsBuy != update.IsBuy)
            {
                throw new InvalidOperationException(string.Format("Can't merge buy and sell candles. Source={0} Update={1}", target.ToJson(), update.ToJson()));
            }
            if (target.DateTime != update.DateTime)
            {
                throw new InvalidOperationException(string.Format("Can't merge candles with different DateTime property. Source={0} Update={1}", target.ToJson(), update.ToJson()));
            }

            return new FeedCandle()
            {
                Open = update.Open,
                Close = update.Close,
                High = Math.Max(target.High, update.High),
                Low = Math.Min(target.Low, update.Low),
                IsBuy = target.IsBuy,
                DateTime = target.DateTime
            };
        }

        public static string ToJson(this IFeedCandle candle)
        {
            return (candle != null)
                ? $"{{ open:{candle.Open} close:{candle.Close} high:{candle.High} low:{candle.Low} isBuy:{candle.IsBuy} dateTime:{candle.DateTime.ToString("o")} }}"
                : string.Empty;
        }
    }
}
