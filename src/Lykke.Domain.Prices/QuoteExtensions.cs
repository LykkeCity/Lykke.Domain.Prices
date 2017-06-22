using System;
using System.Collections.Generic;
using System.Linq;
using Lykke.Domain.Prices.Contracts;
using Lykke.Domain.Prices.Model;

namespace Lykke.Domain.Prices
{
    public static class QuoteExtensions
    {
        public static IFeedCandle ToCandle(this IEnumerable<IQuote> quotes, DateTime dateTime)
        {
            if (quotes == null)
            {
                return null;
            }

            var sorted = quotes.OrderBy(q => q.Timestamp).ToArray();

            if (!sorted.Any())
            {
                return null;
            }

            return new FeedCandle
            {
                Open = sorted.First().Price,
                Close = sorted.Last().Price,
                High = sorted.Max(q => q.Price),
                Low = sorted.Min(q => q.Price),
                IsBuy = sorted.First().IsBuy,
                DateTime = dateTime
            };
        }

        public static IFeedCandle ToCandle(this IQuote quote, DateTime dateTime)
        {
            return new FeedCandle
            {
                Open = quote.Price,
                Close = quote.Price,
                High = quote.Price,
                Low = quote.Price,
                IsBuy = quote.IsBuy,
                DateTime = dateTime
            };
        }

        public static string ToJson(this IQuote quote)
        {
            return quote != null
                ? $"{{ assetPair:{quote.AssetPair}, isBuy:{quote.IsBuy}, price:{quote.Price}, timestamp:{quote.Timestamp:o} }}"
                : string.Empty;
        }
    }
}
