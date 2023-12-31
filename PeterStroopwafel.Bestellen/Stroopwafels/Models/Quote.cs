using System;

namespace Stroopwafels.Models
{
    public class Quote
    {
        public string SupplierName { get; set; } = null!;

        public string TotalAmount { get; set; } = null!;

        public DateOnly DeliveryDate { get; set; } = new DateOnly();
    }
}
