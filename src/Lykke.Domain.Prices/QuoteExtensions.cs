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
        public static IFeedCandle ToCandle(this IEnumerable<Quote> quotes, DateTime dateTime)
        {
            if (quotes == null || !quotes.Any())
            {
                return null;
            }

            var sorted = quotes.OrderBy(q => q.Timestamp).ToList();

            return new FeedCandle()
            {
                Open = sorted.First().Price,
                Close = sorted.Last().Price,
                High = sorted.Max(q => q.Price),
                Low = sorted.Min(q => q.Price),
                IsBuy = sorted.First().IsBuy,
                DateTime = dateTime
            };
        }

        public static string ToJson(this IQuote quote)
        {
            return (quote != null)
                ? $"{{ assetPair:{quote.AssetPair}, isBuy:{quote.IsBuy}, price:{quote.Price}, timestamp:{quote.Timestamp.ToString("o") } }}"
                : string.Empty;
        }
    }
}
