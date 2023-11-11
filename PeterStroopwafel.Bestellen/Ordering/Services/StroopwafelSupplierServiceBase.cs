using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ordering.Services
{
    public abstract class StroopwafelSupplierServiceBase
    {
        protected const string MediaType = "application/json";
        protected readonly IHttpClientWrapper HttpClient;

        protected StroopwafelSupplierServiceBase(IHttpClientWrapper httpClientWrapper)
        {
            HttpClient = httpClientWrapper;
        }

        protected async Task<JsonDocument> ExecuteGetAsync(Uri uri)
        {
            var request = GetRequest(uri, MediaType);
            var content = await HttpClient.GetAsync(request);

            return JsonDocument.Parse(content.ReadAsStringAsync().Result);
        }

        protected async void ExecutePostAsync(Uri uri, Order order)
        {
            var content = GetContent(order);
            var response = await HttpClient.PostAsync(uri, content);
            var result = response.Content.ReadAsStringAsync().Result;
        }

        private HttpContent GetContent(Order order)
        {
            var content = new StringContent(JsonSerializer.Serialize(order));
            content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
            return content;
        }

        private HttpRequestMessage GetRequest(Uri uri, string mediaType)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            return request;
        }
    }
}
