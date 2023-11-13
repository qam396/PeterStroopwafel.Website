using System.Collections.Generic;

namespace Stroopwafels.Models
{
    public class QuoteViewModel
    {
        public IList<OrderRow> OrderRows { get; set; }

        public QuoteViewModel()
        {
            OrderRows = new List<OrderRow>();
        }
    }
}
