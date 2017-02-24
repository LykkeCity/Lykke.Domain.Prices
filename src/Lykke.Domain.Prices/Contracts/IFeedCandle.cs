using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.Domain.Prices.Contracts
{
    public interface IFeedCandle
    {
        DateTime DateTime { get; }
        double Open { get; }
        double Close { get; }
        double High { get; }
        double Low { get; }
        bool IsBuy { get; }
    }
}
