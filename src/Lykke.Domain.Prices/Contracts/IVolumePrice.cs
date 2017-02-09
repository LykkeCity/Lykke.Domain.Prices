using System;

namespace Lykke.Domain.Prices.Contracts
{
    public interface IVolumePrice
    {
        double Volume { get; }
        double Price { get; }
    }
}
