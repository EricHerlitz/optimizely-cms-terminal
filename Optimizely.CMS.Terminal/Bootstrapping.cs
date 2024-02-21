using EPiServer.Data;
using EPiServer.Licensing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Optimizely.CMS.Terminal;

public static class Bootstrapping
{
    public static void BootstrapEpi(this IServiceCollection services, HostBuilderContext hostContext)
    {
        services.Configure<DataAccessOptions>(o =>
        {
            o.ConnectionStrings.Add(new ConnectionStringOptions
            {
                ConnectionString = hostContext.Configuration.GetConnectionString("EPiServerDB"),
                Name = o.DefaultConnectionStringName
            });
        });
        services.Configure<LicensingOptions>(o =>
        {
            o.LicenseFilePath = hostContext.Configuration.GetSection("EPiServer:CMS:LicensePath:Path").Value;
        });
        services.AddCmsHost();
    }
}