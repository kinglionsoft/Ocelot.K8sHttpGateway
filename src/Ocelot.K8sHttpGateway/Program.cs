using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Ocelot.K8sHttpGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseKestrel((hostingContext, options) =>
                {
                    var maxBodySize = hostingContext.Configuration["MaxRequestSize"];
                    if (!string.IsNullOrWhiteSpace(maxBodySize)
                        && int.TryParse(maxBodySize, out var mrbs))
                    {
                        options.Limits.MaxRequestBodySize = mrbs * 1024L * 1024L;
                    }
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile(Path.Combine("config", "appsettings.json"), false, true)
                        .AddJsonFile(Path.Combine("config", $"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json"), true, true)
                        .AddJsonFile(Path.Combine("config", "ocelot.json"), false, true)
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add logging
                    logging.AddConsole();
                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        logging.SetMinimumLevel(LogLevel.Debug);
                    }
                    else
                    {
                        logging.SetMinimumLevel(LogLevel.Error);
                    }
                })
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
