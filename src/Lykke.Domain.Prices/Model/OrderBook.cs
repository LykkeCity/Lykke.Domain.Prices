using System;
using System.Collections.Generic;
using Lykke.Domain.Prices.Contracts;

namespace Lykke.Domain.Prices.Model
{
    public class OrderBook : IOrderBook
    {
        public string AssetPair { get; set; }
        public bool IsBuy { get; set; }
        public DateTime Timestamp { get; set; }
        public IReadOnlyList<IVolumePrice> Prices { get; set; } = new List<VolumePrice>();
    }
}
