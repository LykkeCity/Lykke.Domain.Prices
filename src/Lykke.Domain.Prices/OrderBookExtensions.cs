using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Domain.Prices.Contracts;

namespace Lykke.Domain.Prices
{
    public static class OrderBookExtensions
    {
        public static double GetPrice(this IOrderBook src)
        {
            return src.IsBuy
                ? src.Prices.Max(item => item.Price)
                : src.Prices.Min(item => item.Price);
        }
    }
}
