using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetShop.Api.Pet;
using PetShop.Authorization.Data;
using PetShop.Infrastructure.Data.Context;

namespace PetShopIntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var integrationConfig = new ConfigurationBuilder()
              .AddJsonFile("integrationsettings.json")
              .Build();

            builder.ConfigureAppConfiguration(config =>
            {
                config.AddConfiguration(integrationConfig);
            });

            builder.ConfigureServices(services =>
            {
                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var petshop = scopedServices.GetRequiredService<PetShopDbContext>();


                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();
                    petshop.Database.EnsureCreated();

                }
            });
        }
    }
}
