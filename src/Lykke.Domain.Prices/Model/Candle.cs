using System;
using Lykke.Domain.Prices.Contracts;

namespace Lykke.Domain.Prices.Model
{
    public class Candle : ICandle
    {
        public DateTime DateTime { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public bool IsBuy { get; set; }

        public bool Equals(ICandle other)
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

        public override int GetHashCode()
        {
            return this.DateTime.GetHashCode()
                ^ this.IsBuy.GetHashCode()
                ^ this.Open.GetHashCode()
                ^ this.Close.GetHashCode()
                ^ this.High.GetHashCode()
                ^ this.Low.GetHashCode();
        }
    }
}
