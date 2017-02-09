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

        public override bool Equals(object obj)
        {
            Quote other = obj as Quote;
            if (other == null)
            {
                return false;
            }
            return (string.Equals(this.AssetPair, other.AssetPair, StringComparison.OrdinalIgnoreCase)
                && this.IsBuy == other.IsBuy
                && this.Price == other.Price
                && this.Timestamp == other.Timestamp);
        }

        public override int GetHashCode()
        {
            return (this.AssetPair ?? "").GetHashCode()
                ^ this.IsBuy.GetHashCode()
                ^ this.Price.GetHashCode()
                ^ this.Timestamp.GetHashCode();
        }
    }
}
