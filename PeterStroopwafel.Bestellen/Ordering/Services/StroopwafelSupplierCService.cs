using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ordering.Services
{
    public class StroopwafelSupplierCService : StroopwafelSupplierServiceBase, IStroopwafelSupplierService
    {
        private static readonly Uri ProductsUri = new Uri("http://stroopwafelc.azurewebsites.net/api/Products");
        private static readonly Uri OrderUri = new Uri("http://stroopwafelc.azurewebsites.net/api/Order");

        public ISupplier Supplier => new SupplierC();

        public bool IsAvailable => true;

        public StroopwafelSupplierCService(IHttpClientWrapper httpClientWrapper) : base(httpClientWrapper)
        {
        }

        public Quote GetQuote(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var result = ExecuteGet(ProductsUri);
            var json = result.RootElement.GetRawText();
            var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
            var stroopwafels = JsonSerializer.Deserialize<IList<Stroopwafel>>(json, options);

            var builder = new QuoteBuilder();

            return builder.CreateOrder(orderDetails, stroopwafels!, new SupplierC());
        }

        public void Order(IList<KeyValuePair<StroopwafelType, int>> quoteLines)
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrder(quoteLines);
            ExecutePost(OrderUri, order);
        }
    }
}
