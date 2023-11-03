using System;
using System.Net.Http;

namespace Ordering.Services
{
    public class HttpClientWrapper : IHttpClientWrapper, IDisposable
    {
        private bool _disposed;

        private readonly HttpClient _httpClient;

        public HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public HttpContent Get(HttpRequestMessage request)
        {
            var response = _httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();

            return response.Content;
        }

        public HttpResponseMessage Post(Uri requestUri, HttpContent content)
        {
            var response = _httpClient.PostAsync(requestUri, content).Result;
            response.EnsureSuccessStatusCode();

            return response;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
            {
                return;
            }

            _disposed = true;
            _httpClient?.Dispose();
        }
    }
}
