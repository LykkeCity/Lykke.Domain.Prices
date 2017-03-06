using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Domain.Prices.Contracts;

namespace Lykke.Domain.Prices.Repositories
{
    /// <summary>
    /// Repository interface describing get/insert operations with IFeedCandle objects.
    /// </summary>
    public interface ICandleHistoryRepository
    {
        /// <summary>
        /// Insert or merge candle value.
        /// </summary>
        Task InsertOrMergeAsync(IFeedCandle feedCandle, string asset, TimeInterval interval);

        /// <summary>
        /// Insert or merge candle value.
        /// </summary>
        Task InsertOrMergeAsync(IEnumerable<IFeedCandle> candles, string asset, TimeInterval interval);

        /// <summary>
        /// Insert specified candles for each specified time interval.
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        Task InsertOrMergeAsync(IReadOnlyDictionary<TimeInterval, IEnumerable<IFeedCandle>> candles, string asset);

        /// <summary>
        /// Returns buy or sell candle value for the specified interval in the specified time.
        /// </summary>
        Task<IFeedCandle> GetCandleAsync(string asset, TimeInterval interval, bool isBuy, DateTime dateTime);

        /// <summary>
        /// Returns buy or sell candle values for the specified interval from the specified time range.
        /// </summary>
        Task<IEnumerable<IFeedCandle>> GetCandlesAsync(string asset, TimeInterval interval, bool isBuy, DateTime from, DateTime to);
    }
}
