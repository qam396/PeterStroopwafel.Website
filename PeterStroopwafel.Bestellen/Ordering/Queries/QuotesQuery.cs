using System.Collections.Generic;

namespace Ordering.Queries
{
    public class QuotesQuery
    {
        public KeyValuePair<StroopwafelType, int> OrderLine { get; }

        public QuotesQuery(KeyValuePair<StroopwafelType, int> orderLine)
        {
            OrderLine = orderLine;
        }
    }
}
