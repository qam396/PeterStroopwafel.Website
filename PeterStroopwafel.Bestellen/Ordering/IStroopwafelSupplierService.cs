using System.Collections.Generic;

namespace Ordering
{
    public interface IStroopwafelSupplierService
    {
        ISupplier Supplier { get; }

        bool IsAvailable { get; }

        Quote GetQuote(KeyValuePair<StroopwafelType, int> orderDetail);

        void Order(IList<KeyValuePair<StroopwafelType, int>> quote);
    }
}
