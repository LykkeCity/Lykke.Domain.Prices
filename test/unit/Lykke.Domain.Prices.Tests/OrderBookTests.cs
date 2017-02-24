using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Lykke.Domain.Prices.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Lykke.Domain.Prices.Tests
{
    public class OrderBookTests
    {
        [Fact]
        public void OrderCanBeSerializedToJson()
        {
            var order = new OrderBook()
            {
                AssetPair = "BTCUSD",
                IsBuy = true,
                Timestamp = new DateTime(2009, 2, 15, 15, 2, 0),
                Prices = new List<VolumePrice>
                {
                    new VolumePrice() { Volume = 0.52, Price = 933.89 },
                    new VolumePrice() { Volume = 43.78618975, Price = 933.88 },
                    new VolumePrice() { Volume = 6.4028, Price = 933.62 },
                }
            };

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string json = JsonConvert.SerializeObject(order, settings);

            Assert.Equal("{\"assetPair\":\"BTCUSD\",\"isBuy\":true,\"timestamp\":\"2009-02-15T15:02:00\","
                + "\"prices\":[{\"volume\":0.52,\"price\":933.89},{\"volume\":43.78618975,\"price\":933.88},{\"volume\":6.4028,\"price\":933.62}]}",
                json);
        }

        [Fact]
        public void OrderCanBeDeserializedFromJson()
        {
            string json = "{\"assetPair\":\"BTCUSD\",\"isBuy\":true,\"timestamp\":\"2009-02-15T15:02:00Z\","
                + "\"prices\":[{\"volume\":0.52,\"price\":933.89},{\"volume\":43.78618975,\"price\":933.88},{\"volume\":6.4028,\"price\":933.62}]}";

            var order = JsonConvert.DeserializeObject<OrderBook>(json);

            Assert.NotNull(order);
            Assert.Equal("BTCUSD", order.AssetPair);
            Assert.True(order.IsBuy);
            Assert.Equal(new DateTime(2009, 2, 15, 15, 2, 0), order.Timestamp);
            Assert.NotNull(order.Prices);
            Assert.Equal(3, order.Prices.Count);
            Assert.Equal(0.52, order.Prices[0].Volume);
            Assert.Equal(933.89, order.Prices[0].Price);
        }
    }
}
