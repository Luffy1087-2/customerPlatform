using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CustomerPlatform.WebApi.Tools
{
    internal static class StartupUtility
    {
        private const string WebProjectName = "CustomerPlatform.WebApi";

        public static string GetStartupAssemblies()
        {
           List<string?> typesString = Directory
                .GetFiles(Environment.CurrentDirectory, "CustomerPlatform.*.dll", SearchOption.AllDirectories).ToList()
                .GroupBy(Path.GetFileNameWithoutExtension)
                .Where(dll => !dll.Key?.Equals(WebProjectName) == true)
                .Select(f => Assembly.LoadFile(f.First()))
                .Select(a => a.FullName?.Split(",")[0])
                .ToList();

           string joinedString = string.Join(";", typesString);

           return joinedString;
        }
    }
}
