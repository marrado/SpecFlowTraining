using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

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
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
                                          {
                                              AllowAutoRedirect = false,
                                              HandleCookies = true
                                          });
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
    }
}