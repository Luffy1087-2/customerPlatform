#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CustomerPlatform.Data.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IStartup = CustomerPlatform.Core.Abstract.IStartup;

namespace CustomerPlatform.WebApi.Startup
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
            ConfigureAllReferenceServices(services, Configuration);
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

        private static void ConfigureAllReferenceServices(IServiceCollection services, IConfiguration configuration)
        {
            IEnumerable<Type> startupTypes = GetReferencedAssemblyStartupTypes();
            foreach (Type startupType in startupTypes)
            {
                object? instance = Activator.CreateInstance(startupType);
                startupType.InvokeMember("ConfigureServices", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, instance, new object?[] { services });
            }
        }

        private static IEnumerable<Type> GetReferencedAssemblyStartupTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(t => typeof(IStartup).IsAssignableFrom(t) && t.IsClass)
                .ToList();
        }
    }
}
