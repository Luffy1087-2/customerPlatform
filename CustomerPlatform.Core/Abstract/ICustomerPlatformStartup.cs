using Microsoft.Extensions.DependencyInjection;

namespace CustomerPlatform.Core.Abstract
{
    public interface ICustomerPlatformStartup
    {
        void ConfigureServices(IServiceCollection services);
    }
}
