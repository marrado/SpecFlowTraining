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
    public class CustomWebApplicationFactory
        : WebApplicationFactory<Startup>
    {
        private ServiceProvider _serviceProvider;

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
                                          
                                          // Add memory cache services
                                          services.AddMemoryCache();

                                          // Build the service provider.
                                          _serviceProvider = services.BuildServiceProvider();

                                          // Create a scope to obtain a reference to the database
                                          // context (ApplicationDbContext).
                                          using (var scope = _serviceProvider.CreateScope())
                                          {
                                              var scopedServices = scope.ServiceProvider;
                                              var db = scopedServices.GetRequiredService<CatalogContext>();
                                              // Ensure the database is created.
                                              db.Database.EnsureCreated();
                                          }
                                      });
        }
    }
}
