using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering
{
    public class Quote
    {
        public decimal TotalPrice => TotalWithoutShippingCost + Supplier.GetShippingCost(this);

        public string TotalPricePresentation => TotalPrice.ToString("C");

        public decimal TotalWithoutShippingCost
        {
            get { return OrderLine.Price; }
        }

        public QuoteLine OrderLine { get; }

        public ISupplier Supplier { get; }

        public DateOnly DeliveryDate { get; }

        public Quote(QuoteLine orderLine, ISupplier supplier, DateOnly deliveryDate)
        {
            OrderLine = orderLine;
            Supplier = supplier;
            DeliveryDate = deliveryDate;
        }
    }
}
