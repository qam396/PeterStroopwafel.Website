using System.Text.Json.Serialization;

namespace Ordering
{
    public class Stroopwafel
    {
        [JsonPropertyName("Type")]
        public StroopwafelType Type { get; set; }

        [JsonPropertyName("Merk")]
        public Brand Brand { get; set; }

        [JsonPropertyName("Prijs")]
        public decimal Price { get; set; }

        [JsonConstructor]
        public Stroopwafel(StroopwafelType type, Brand brand, decimal price)
        {
            Type = type;
            Brand = brand;
            Price = price;
        }

        public Stroopwafel(StroopwafelType type)
        {
            Type = type;
        }
    }
}
