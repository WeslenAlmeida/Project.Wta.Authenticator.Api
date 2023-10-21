using Microsoft.Extensions.Configuration;

namespace CrossCutting.Configuration
{
    internal static class ConfigurationAppSettings
    {
        internal static IConfigurationRoot Builder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

                return builder.Build();
        }
    }
}