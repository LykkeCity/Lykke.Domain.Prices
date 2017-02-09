using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.Domain.Prices.Contracts
{
    public interface IQuote
    {
        string AssetPair { get; }
        bool IsBuy { get; }
        double Price { get; }
        DateTime Timestamp { get; }
    }
}
