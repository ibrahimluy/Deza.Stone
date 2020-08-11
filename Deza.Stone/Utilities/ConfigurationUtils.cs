using Microsoft.Extensions.Configuration;
using System.IO;

namespace Deza.Stone.Utilities
{
    public class ConfigurationUtils
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static T GetSection<T>(string sectionName)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory()) // requires Microsoft.Extensions.Configuration.Json
                  .AddJsonFile("appsettings.json") // requires Microsoft.Extensions.Configuration.Json
                  .AddEnvironmentVariables(); // requires Microsoft.Extensions.Configuration.EnvironmentVariables

            Configuration = builder.Build();

            var result = Configuration.GetSection(sectionName).Get<T>();

            return result;

        }
    }
}
