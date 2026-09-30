using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace POS.Test;

//Allow to acces configuration and services from the main project for integration testing
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    // You can override methods here to customize the test server configuration if needed
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(services =>
        {
           var integrationConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            services.AddConfiguration(integrationConfiguration);
        });
    }

}