using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerPlatform.WebApi.Tools
{
    internal static class StartupUtility
    {
         private const string WebProjectName = "CustomerPlatform.WebApi";

        public static void ConfigureAllServices(IServiceCollection services)
        {
            IEnumerable<Type> startupTypes = GetCustomerPlatformTypes();
            foreach (Type startupType in startupTypes)
            {
                object? instance = Activator.CreateInstance(startupType);
                startupType.InvokeMember("ConfigureServices", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, instance, new object?[] { services });
            }
        }

        private static IEnumerable<Type> GetCustomerPlatformTypes()
        {
            return Directory
                .GetFiles(Environment.CurrentDirectory, "CustomerPlatform.*.dll", SearchOption.AllDirectories).ToList()
                .GroupBy(Path.GetFileNameWithoutExtension)
                .Where(dll => !dll.Key?.Equals(WebProjectName) == true)
                .Select(f => Assembly.LoadFile(f.First()))
                .SelectMany(assembly => assembly.GetTypes())
                //.Where(t => t.IsClass && typeof(ICustomerPlatformStartup).IsAssignableFrom(t))
                .Where(t => t.IsClass && t.Name == "CustomerPlatformStartup")
                .ToList();
        }
    }
}
