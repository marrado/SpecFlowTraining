using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SpecFlowTests.Infrastructure
{
    internal class SimpleCookieContainer : DelegatingHandler
    {
        internal SimpleCookieContainer(CookieContainer cookieContainer = null)
        {
            this.Container = cookieContainer ?? new CookieContainer();
        }

        internal SimpleCookieContainer(HttpMessageHandler innerHandler, CookieContainer cookieContainer = null)
            : base(innerHandler)
        {
            this.Container = cookieContainer ?? new CookieContainer();
        }

        public CookieContainer Container { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            this.Container.ApplyCookies(request);
            var response = await base.SendAsync(request, cancellationToken);
            this.Container.SetCookies(response);
            return response;
        }
    }

    internal static class CookieContainerExtensions
    {
        internal static void SetCookies(this CookieContainer container, HttpResponseMessage response, Uri requestUri = null)
        {
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieHeaders))
            {
                foreach (string cookie in cookieHeaders)
                {
                    container.SetCookies(requestUri ?? response.RequestMessage.RequestUri, cookie);
                }
            }
        }

        internal static void ApplyCookies(this CookieContainer container, HttpRequestMessage request)
        {
            string cookieHeader = container.GetCookieHeader(request.RequestUri);
            if (!string.IsNullOrEmpty(cookieHeader))
            {
                request.Headers.TryAddWithoutValidation("Cookie", cookieHeader);
            }
        }
    }
}