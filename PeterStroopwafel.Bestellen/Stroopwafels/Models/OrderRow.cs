using Ordering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stroopwafels.Models
{
    public class OrderRow
    {
        [Required]
        public int Amount { get; set; }

        public StroopwafelType Type { get; set; }

        public IList<Quote> Quotes { get; set; }

        public string SelectedSupplier { get; set; }

        public string DisplayName => Type.ToString();

        public OrderRow()
        {
        }

        public OrderRow(int amount, StroopwafelType type, List<Quote> quotes, string selectedSupplier)
        {
            Amount = amount;
            Type = type;
            Quotes = quotes;
            SelectedSupplier = selectedSupplier;
        }
    }
}
