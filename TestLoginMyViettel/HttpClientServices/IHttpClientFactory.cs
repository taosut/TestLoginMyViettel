using System.Net.Http;

namespace TestLoginMyViettel.HttpClientServices
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient { get; }
    }
}