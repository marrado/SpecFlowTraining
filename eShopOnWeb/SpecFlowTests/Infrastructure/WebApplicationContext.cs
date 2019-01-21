using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SpecFlowTests.Infrastructure
{
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

        public void PerformServiceAction<T>(Action<T> action)
        {
            _factory.PerformServiceAction(action);
        }
    }
}