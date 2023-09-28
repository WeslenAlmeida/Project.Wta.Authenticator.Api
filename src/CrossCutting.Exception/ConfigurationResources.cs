using Microsoft.Extensions.Configuration;

namespace CrossCutting.Exception
{
    internal static class ConfigurationResources
    {
        internal static IConfigurationRoot Builder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("resources.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        internal static string? GetExceptionMessage(string customException)
        {
            var resources = Builder().GetSection("resource");

            foreach (IConfigurationSection section in resources.GetChildren())
            {
                if (section.GetValue<string>("key") == customException)
                    return  section?.GetValue<string>("value");
            }
            return null;
        }
    }
}