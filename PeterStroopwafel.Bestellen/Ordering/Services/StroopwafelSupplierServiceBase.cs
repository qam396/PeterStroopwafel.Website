using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

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

        protected JsonDocument ExecuteGet(Uri uri)
        {
            var request = GetRequest(uri, MediaType);
            var content = HttpClient.Get(request);

            return JsonDocument.Parse(content.ReadAsStringAsync().Result);
        }

        protected void ExecutePost(Uri uri, Order order)
        {
            var content = GetContent(order);
            var response = HttpClient.Post(uri, content);
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
