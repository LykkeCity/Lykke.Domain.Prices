using System;
using Lykke.Domain.Prices.Contracts;

namespace Lykke.Domain.Prices.Model
{
    public class VolumePrice : IVolumePrice
    {
        public double Volume { get; set; }
        public double Price { get; set; }
    }
}
