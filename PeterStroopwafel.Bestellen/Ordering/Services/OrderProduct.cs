using System.Text.Json.Serialization;

namespace Ordering.Services
{
    public class OrderProduct
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StroopwafelType Type { get; }

        [JsonPropertyName("Merk")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Brand Brand { get; }

        public OrderProduct(StroopwafelType type)
        {
            Type = type;
            Brand = Brand.Stroopie;
        }
    }
}
