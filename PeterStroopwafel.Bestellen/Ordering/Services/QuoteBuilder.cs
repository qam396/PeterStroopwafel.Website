using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Services
{
    public class QuoteBuilder
    {
        public Quote CreateOrder(KeyValuePair<StroopwafelType, int> orderDetail, IList<Stroopwafel> stroopwafels, ISupplier supplier, DateOnly deliveryDate)
        {
            var stroopwafel = stroopwafels.First(s => s.Type == orderDetail.Key);
            var orderLine = new QuoteLine(orderDetail.Value, stroopwafel);

            return new Quote(orderLine, supplier, deliveryDate);
        }
    }
}
