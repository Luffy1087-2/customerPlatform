using CustomerPlatform.Binders;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Factory;
using CustomerPlatform.Data.Abstract;
using CustomerPlatform.Data.Clients;
using CustomerPlatform.Data.Configuration;
using CustomerPlatform.Data.Providers;
using CustomerPlatform.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CustomerPlatform
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<CustomersDbConfiguration>(Configuration);
            services.AddSingleton<ICustomerFactory, CustomerFactory>();
            services.AddSingleton<ICustomerDataProvider, CustomerDataProvider>();
            services.AddSingleton<ICustomersDataRepository, CustomersDataRepository>();
            services.AddSingleton<ICustomersDbClient, CustomersDbClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
