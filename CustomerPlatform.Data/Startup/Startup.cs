using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Data.Abstract;
using CustomerPlatform.Data.Clients;
using CustomerPlatform.Data.Providers;
using CustomerPlatform.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerPlatform.Data.Startup
{
    public class Startup : IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICustomersDbClient, CustomersDbClient>();
            services.AddSingleton<ICustomerDataProvider, CustomerDataProvider>();
            services.AddSingleton<ICustomersDataRepository, CustomersDataRepository>();
        }
    }
}
