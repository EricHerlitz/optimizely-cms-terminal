using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace Optimizely.CMS.Terminal.Examples;

public class InstanceCounter(
    IContentTypeRepository contentTypeRepository,
    IContentModelUsage contentModelUsage)
{
    public void CountInstances()
    {
        var contentTypes = contentTypeRepository.List();
        var items = contentTypes.ToList().Select(contentType => new
        {
            Name = contentType.Name,
            Count = contentModelUsage.ListContentOfContentType(contentType).DistinctBy(x => x.ContentLink.ID).Count()
        }).ToList();

        items.ForEach(item => Console.WriteLine($"{item.Name} has {item.Count} instances"));
    }
}