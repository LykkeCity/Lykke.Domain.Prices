using System;
using System.Collections.Generic;

namespace Lykke.Domain.Prices.Contracts
{
    public interface IOrder
    {
        string AssetPair { get; }
        bool IsBuy { get; }
        DateTime Timestamp { get; }
        IReadOnlyList<IVolumePrice> Prices { get; }
    }
}
