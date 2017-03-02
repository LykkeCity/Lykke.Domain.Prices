using System;
using Lykke.Domain.Prices.Contracts;

namespace Lykke.Domain.Prices.Model
{
    public class Quote : IQuote
    {
        public string AssetPair { get; set; }
        public bool IsBuy { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
