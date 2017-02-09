using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Lykke.Domain.Prices.Contracts;
using Lykke.Domain.Prices.Model;

namespace Lykke.Domain.Prices
{
    public static class QuoteExtensions
    {
        public static ICandle ToCandle(this IEnumerable<Quote> quotes, DateTime dateTime)
        {
            if (quotes == null || !quotes.Any())
            {
                return null;
            }

            var sorted = quotes.OrderBy(q => q.Timestamp).ToList();

            return new Candle()
            {
                Open = sorted.First().Price,
                Close = sorted.Last().Price,
                High = sorted.Max(q => q.Price),
                Low = sorted.Min(q => q.Price),
                IsBuy = sorted.First().IsBuy,
                DateTime = dateTime
            };
        }

        public static string ToString(this IQuote quote)
        {
            return (quote != null)
                ? $"{{ asset:{quote.AssetPair} buy:{quote.IsBuy} price:{quote.Price} t:{quote.Timestamp} }}"
                : string.Empty;
        }
    }
}
