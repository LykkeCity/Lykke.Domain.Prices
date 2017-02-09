using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.Domain.Prices.Contracts
{
    public interface ICandleHistoryRepository
    {
        Task InsertOrMergeAsync(ICandle feedCandle, string asset, TimeInterval interval);
        Task<ICandle> GetCandleAsync(string asset, TimeInterval interval, bool isBuy, DateTime dateTime);
        //Task<IEnumerable<ICandle>> GetCandlesAsync(string asset, TimeInterval interval, bool isBuy, DateTime dateTime);
        Task<IEnumerable<ICandle>> GetCandlesAsync(string asset, TimeInterval interval, bool isBuy, DateTime from, DateTime to);
    }
}
