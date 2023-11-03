using System.Collections.Generic;

namespace Ordering.Services
{
    public class Order
    {
        public IList<OrderLine> ProductsAndAmounts { get; }

        public Order(IList<OrderLine> productsAndAmounts)
        {
            ProductsAndAmounts = productsAndAmounts;
        }
    }
}
