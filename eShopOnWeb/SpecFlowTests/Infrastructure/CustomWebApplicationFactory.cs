using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SpecFlowTests.Infrastructure
{
    /// <summary>
    /// This class sets up a fake WebApplication to be used during the tests
    /// </summary>
    public class CustomWebApplicationFactory
        : WebApplicationFactory<Startup>
    {
        private ServiceProvider _serviceProvider;

        /// <summary>
        /// Perform action using a specified service in the WebApplicaiton
        /// </summary>
        /// <typeparam name="T">Service-Type</typeparam>
        /// <param name="action">Action to be performed</param>
        public void PerformServiceAction<T>(Action<T> action)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var service = scopedServices.GetRequiredService<T>();
                action(service);
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
                                      {
                                          services.ConfigureApplicationCookie(options =>
                                                                              {
                                                                                  options.Cookie.HttpOnly = true;
                                                                                  options.ExpireTimeSpan = TimeSpan.FromHours(1);
                                                                                  options.LoginPath = "/Account/Signin";
                                                                                  options.LogoutPath = "/Account/Signout";
                                                                                  options.Cookie = new CookieBuilder
                                                                                                   {
                                                                                                       IsEssential = true // required for auth to work without explicit user consent; adjust to suit your privacy policy
                                                                                                   };
                                                                              });
                                          // Create a new service provider.
                                          var serviceProvider = new ServiceCollection()
                                                                .AddEntityFrameworkInMemoryDatabase()
                                                                .BuildServiceProvider();

                                          // Add a database context (ApplicationDbContext) using an in-memory 
                                          // database for testing.
                                          services.AddDbContext<CatalogContext>(options =>
                                                                                {
                                                                                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                                                                                    options.UseInternalServiceProvider(serviceProvider);
                                                                                });

                                          services.AddDbContext<AppIdentityDbContext>(options =>
                                                                                      {
                                                                                          options.UseInMemoryDatabase("Identity");
                                                                                          options.UseInternalServiceProvider(serviceProvider);
                                                                                      });
                                          
                                          // Build the service provider.
                                          _serviceProvider = services.BuildServiceProvider();

                                          // Ensure the database is created.
                                          PerformServiceAction<CatalogContext>(db => db.Database.EnsureCreated());
                                      });
        }
    }
}
