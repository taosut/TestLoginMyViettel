using System;
using System.Net;
using System.Net.Http;
using TestLoginMyViettel.Common;

namespace TestLoginMyViettel.HttpClientServices
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private static HttpClient _client;

        #region Implementation of IHttpClientFactory

        public HttpClient CreateClient
        {
            get
            {
                if (_client == null)
                {
                    var proxy = new WebProxy("127.0.0.1", 24000);
                    var httpHandler = new HttpClientHandler
                    {
                        UseProxy                                  = false,
                        Proxy                                     = null,
                        UseCookies                                = true,
                        CookieContainer                           = new CookieContainer(),
                        AllowAutoRedirect                         = false,
                        AutomaticDecompression                    = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                        ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                    };

                    _client = new HttpClient(httpHandler)
                    {
                        BaseAddress = new Uri(AppConstant.ApiMyViettel),
                        Timeout     = TimeSpan.FromSeconds(60),
                        DefaultRequestHeaders =
                        {
                            {"Connection", "keep-alive"}
                        }
                    };
                }

                return _client;
            }
        }

        #endregion
    }
}