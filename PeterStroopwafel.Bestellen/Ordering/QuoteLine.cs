using System.Text.Json.Serialization;

namespace Ordering
{
    public class QuoteLine
    {
        public int Amount { get; }

        [JsonPropertyName("Product")]
        public Stroopwafel Stroopwafel { get; }

        public decimal Price => Amount * Stroopwafel.Price;

        public QuoteLine(int amount, Stroopwafel stroopwafel)
        {
            Amount = amount;
            Stroopwafel = stroopwafel;
        }
    }
}
