using System;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Data.Abstract;
using CustomerPlatform.Data.Clients;
using CustomerPlatform.Data.Providers;
using CustomerPlatform.Data.Repositories;
using CustomerPlatform.Data.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CustomerPlatformStartup))]

namespace CustomerPlatform.Data.Startup
{
    public class CustomerPlatformStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.ConfigureServices(services =>
            {
                services.AddMemoryCache();
                services.AddSingleton<ICustomersDbClient, CustomersDbClient>();
                services.AddSingleton<ICustomerDataProvider, CustomerDataProvider>();
                services.AddSingleton<ICustomersDataRepository, CustomersDataRepository>();
            });
        }
    }
}
