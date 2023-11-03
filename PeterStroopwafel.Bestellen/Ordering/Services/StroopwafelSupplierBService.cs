using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ordering.Services
{
    public class StroopwafelSupplierBService : StroopwafelSupplierServiceBase, IStroopwafelSupplierService
    {
        private static readonly Uri ProductsUri = new Uri("http://stroopwafelb.azurewebsites.net/api/Products");
        private static readonly Uri OrderUri = new Uri("http://stroopwafelb.azurewebsites.net/api/Order");

        public ISupplier Supplier => new SupplierB();

        private static readonly IList<DateTime> PublicHolidays = new List<DateTime>
        {
            new DateTime(2016, 1, 1),
            new DateTime(2016, 12, 25),
            new DateTime(2016, 12, 26)
        };

        public bool IsAvailable => GetAvailability();

        public StroopwafelSupplierBService(IHttpClientWrapper httpClientWrapper) : base(httpClientWrapper)
        {
        }

        private bool GetAvailability()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            var isHoliday = PublicHolidays.Any(h => h == DateTime.Now.Date);
            return !isHoliday;
        }

        public Quote GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            if (!IsAvailable)
            {
                return null!;
            }

            var result = ExecuteGet(ProductsUri);
            var json = result.RootElement.GetRawText();
            var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
            var stroopwafels = JsonSerializer.Deserialize<IList<Stroopwafel>>(json, options);

            var builder = new QuoteBuilder();
            return builder.CreateOrder(orderDetails, stroopwafels!, new SupplierB());
        }

        public void Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            ExecutePost(OrderUri, order);
        }
    }
}
