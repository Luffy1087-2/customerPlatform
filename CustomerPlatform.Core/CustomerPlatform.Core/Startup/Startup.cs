using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerPlatform.Core.Startup
{
    public class Startup : IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomerFactory, CustomerFactory>();
        }
    }
}
