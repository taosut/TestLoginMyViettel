using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TestLoginMyViettel.HttpClientServices
{
    public static class HttpClientExtension
    {
        public static async Task<HttpResponseMessage> SendAsync(this HttpClient client, string url, HttpContent content = null, Dictionary<string, object> headers = null,
                                                                CancellationToken ct = default)
        {
            ServicePointManager.ServerCertificateValidationCallback                    += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.FindServicePoint(client.BaseAddress).Expect100Continue =  false;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.ExpectContinue = false;

            var request = new HttpRequestMessage
            {
                RequestUri = url != null ? new Uri(url, UriKind.RelativeOrAbsolute) : null,
                Method     = content != null ? HttpMethod.Post : HttpMethod.Get,
                Content    = content
            };

            if (headers != null)
                foreach (var header in headers)
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToString());

            var response = await client.SendAsync(request, ct).ConfigureAwait(false);
            return response;
        }
    }
}