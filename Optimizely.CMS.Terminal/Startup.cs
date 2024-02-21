using EPiServer;
using Microsoft.Extensions.Hosting;
using EPiServer.Core;
using Optimizely.CMS.Terminal.Examples;

namespace Optimizely.CMS.Terminal;

public class Startup(
    InstanceCounter instanceCounter, 
    IContentLoader contentLoader,
    IHostApplicationLifetime hostApplicationLifetime) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine();
        instanceCounter.CountInstances();

        var startPage = contentLoader.Get<PageData>(ContentReference.StartPage);
        var teaserText = startPage.GetPropertyValue<string>("TeaserText");

        Console.WriteLine();
        Console.WriteLine($"Propertydata from the '{startPage.Name}' page");
        Console.WriteLine($"TeaserText: {teaserText}");
        
        hostApplicationLifetime.StopApplication();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        hostApplicationLifetime.StopApplication();
        return Task.CompletedTask;
    }
}