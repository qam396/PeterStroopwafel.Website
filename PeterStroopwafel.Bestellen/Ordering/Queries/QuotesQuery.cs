using System.Collections.Generic;

namespace Ordering.Queries
{
    public class QuotesQuery
    {
        public IList<KeyValuePair<StroopwafelType, int>> OrderLines { get; }

        public QuotesQuery(IList<KeyValuePair<StroopwafelType, int>> orderLines)
        {
            OrderLines = orderLines;
        }
    }
}
