using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ordering.Services
{
    public class HttpClientWrapper : IHttpClientWrapper
    {

        private readonly HttpClient _httpClient;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpContent> GetAsync(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response.Content;
        }

        public async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        {
            var response = await _httpClient.PostAsync(requestUri, content);
            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
