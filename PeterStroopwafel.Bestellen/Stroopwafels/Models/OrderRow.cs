using Ordering;
using System.ComponentModel.DataAnnotations;

namespace Stroopwafels.Models
{
    public class OrderRow
    {
        [Required]
        public int Amount { get; set; }

        public StroopwafelType Type { get; set; }

        public string DisplayName => Type.ToString();

        public OrderRow()
        {
        }

        public OrderRow(int amount, StroopwafelType type)
        {
            Amount = amount;
            Type = type;
        }
    }
}
