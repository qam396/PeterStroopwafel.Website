using Ordering;
using System.Collections.Generic;

namespace Stroopwafels.Models
{
    public class OrderDetailsViewModel
    {
        public IList<OrderRow> OrderRows { get; set; }

        public OrderDetailsViewModel()
        {
            OrderRows = new List<OrderRow>
            {
                new OrderRow(0, StroopwafelType.Gewoon),
                new OrderRow(0, StroopwafelType.Suikervrij),
                new OrderRow(0, StroopwafelType.Super)
            };
        }
    }
}
