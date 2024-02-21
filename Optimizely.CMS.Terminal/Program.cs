using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Optimizely.CMS.Terminal.Examples;

namespace Optimizely.CMS.Terminal;

public class Program
{
    public static async Task Main(string[] args) => await CreateHostBuilder(args).RunConsoleAsync();
    
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        if (args.Any(arg => arg.Equals("Development", StringComparison.CurrentCultureIgnoreCase)))
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        }

        return Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(host =>
            {
                host.AddEnvironmentVariables("ASPNETCORE_");
                host.AddJsonFile("appsettings.json", false);
                host.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    true);
            })
            .ConfigureCmsDefaults()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.BootstrapEpi(hostContext);
                services.AddTransient<InstanceCounter>();
                services.AddHostedService<Startup>();
            });
    }
}
