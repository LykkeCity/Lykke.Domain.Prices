using System;

namespace Lykke.Domain.Prices
{
    public enum TimeInterval
    {
        Unspecified = 0,
        Sec = 1,
        Minute = 60,
        Min5 = 300,
        Min15 = 900,
        Min30 = 1800,
        Hour = 3600,
        Day = 86400,
        Month = 3000000
    }
}
