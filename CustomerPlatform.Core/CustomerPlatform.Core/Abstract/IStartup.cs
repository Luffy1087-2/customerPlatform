using Microsoft.Extensions.DependencyInjection;

namespace CustomerPlatform.Core.Abstract
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);
    }
}
