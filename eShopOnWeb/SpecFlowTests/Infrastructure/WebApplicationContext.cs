using System;
using System.Net;
using System.Net.Http;
using Microsoft.eShopWeb.Web;

namespace SpecFlowTests.Infrastructure
{
    /// <summary>
    /// Context class used to call the Test-Service
    /// </summary>
    public class WebApplicationContext
    {
        public HttpClient Client { get; }

        private readonly CustomWebApplicationFactory _factory;

        public WebApplicationContext(CustomWebApplicationFactory factory)
        {
            var fakeCookieContainer = CreateFakeCookieContainer();
            Client = factory.CreateDefaultClient(new Uri("http://localhost"), fakeCookieContainer);
            _factory = factory;
        }

        /// <summary>
        /// Perform action using a specified service in the WebApplicaiton
        /// </summary>
        /// <typeparam name="T">Service-Type</typeparam>
        /// <param name="action">Action to be performed</param>
        public void PerformServiceAction<T>(Action<T> action)
        {
            _factory.PerformServiceAction(action);
        }

        private static SimpleCookieContainer CreateFakeCookieContainer()
        {
            var container = new CookieContainer();
            container.Add(new Cookie(Constants.BASKET_COOKIENAME, TestConstants.TestUserId)
            {
                Domain = "localhost"
            });
            var fakeCookieContainer = new SimpleCookieContainer(container);
            return fakeCookieContainer;
        }
    }
}