﻿using System;
using Lykke.Domain.Prices.Contracts;

namespace Lykke.Domain.Prices.Model
{
    public class FeedCandle : IFeedCandle, IEquatable<FeedCandle>
    {
        public DateTime DateTime { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public bool IsBuy { get; set; }

        public bool Equals(FeedCandle other)
        {
            if (other != null) {
                return this.DateTime == other.DateTime
                    && this.Open == other.Open
                    && this.Close == other.Close
                    && this.High == other.High
                    && this.Low == other.Low
                    && this.IsBuy == other.IsBuy;
            }
            return false;
        }
    }
}
