﻿using System;
using CustomerPlatform.Core.Abstract;
using CustomerPlatform.Core.Factory;
using CustomerPlatform.Core.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CustomerPlatformStartup))]

namespace CustomerPlatform.Core.Startup
{
    public class CustomerPlatformStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentException(nameof(builder));

            builder.ConfigureServices(services =>
            {
                services.AddSingleton<ICustomerFactory, CustomerFactory>();
            });
        }
    }
}
