using System.Collections.Generic;
using System.Linq;

namespace Ordering.Queries
{
    public class QuotesQueryHandler
    {
        private readonly IEnumerable<IStroopwafelSupplierService> _stroopwafelSupplierServices;

        public QuotesQueryHandler(IEnumerable<IStroopwafelSupplierService> stroopwafelSupplierServices)
        {
            _stroopwafelSupplierServices = stroopwafelSupplierServices;
        }

        public IList<Quote> Handle(QuotesQuery query)
        {
            return _stroopwafelSupplierServices
                .Where(service => service.IsAvailable)
                .Select(service => service.GetQuote(query.OrderLine))
                .ToList();
        }
    }
}
