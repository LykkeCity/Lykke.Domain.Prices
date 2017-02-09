using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Lykke.Domain.Prices.Contracts;
using Lykke.Domain.Prices.Model;

namespace Lykke.Domain.Prices
{
    public static class CandleExtensions
    {
        public static ICandle MergeWith(this ICandle target, ICandle update)
        {
            if (target == null || update == null)
            {
                return target ?? update;
            }

            if (target.IsBuy != update.IsBuy)
            {
                throw new InvalidOperationException(string.Format("Can't merge buy and sell candles. Source={0} Update={1}", target.ToString(), update.ToString()));
            }
            if (target.DateTime != update.DateTime)
            {
                throw new InvalidOperationException(string.Format("Can't merge candles with different DateTime property. Source={0} Update={1}", target.ToString(), update.ToString()));
            }

            return new Candle()
            {
                Open = update.Open,
                Close = update.Close,
                High = Math.Max(target.High, update.High),
                Low = Math.Min(target.Low, update.Low),
                IsBuy = target.IsBuy,
                DateTime = target.DateTime
            };
        }

        public static string ToString(this ICandle candle)
        {
            return (candle != null)
                ? $"{{ o:{candle.Open} c:{candle.Close} h:{candle.High} l:{candle.Low} buy:{candle.IsBuy} dt:{candle.DateTime} }}"
                : string.Empty;
        }
    }
}
