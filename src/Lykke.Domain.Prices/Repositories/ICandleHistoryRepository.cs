using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Domain.Prices.Contracts;

namespace Lykke.Domain.Prices.Repositories
{
    public interface ICandleHistoryRepository
    {
        Task InsertOrMergeAsync(IFeedCandle feedCandle, string asset, TimeInterval interval);
        Task InsertOrMergeAsync(IEnumerable<IFeedCandle> candles, string asset, TimeInterval interval);
        Task<IFeedCandle> GetCandleAsync(string asset, TimeInterval interval, bool isBuy, DateTime dateTime);
        Task<IEnumerable<IFeedCandle>> GetCandlesAsync(string asset, TimeInterval interval, bool isBuy, DateTime from, DateTime to);
    }
}
