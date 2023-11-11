using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ordering.Services
{
    public interface IHttpClientWrapper
    {
        Task<HttpContent> GetAsync(HttpRequestMessage request);

        Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content);
    }
}
