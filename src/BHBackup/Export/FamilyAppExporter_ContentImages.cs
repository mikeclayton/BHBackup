using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Client.GraphQl.Observations.Models;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private async Task DownloadContentImages(
        IEnumerable<FeedItem> feedItems, bool overwrite
    )
    {
        var feedImages = feedItems
            .SelectMany(feedItem => feedItem.Images)
            .OrderBy(feedImage => feedImage.OfflineUrl)
            .ToList();
        Console.WriteLine("downloading feed item content images...");
        foreach (var feedImage in feedImages)
        {
            await this.DownloadHttpResource(
                feedImage.UrlBig, feedImage.OfflineUrl, overwrite
            );
        }
    }

    private async Task DownloadContentImages(
        IEnumerable<Observation> observations, bool overwrite
    )
    {
        var images = observations
            .SelectMany(observation => observation.Images)
            .OrderBy(image => image.Secret.OfflineUrl)
            .ToList();
        Console.WriteLine("downloading observation content images...");
        foreach (var image in images)
        {
            await this.DownloadHttpResource(
                image.Secret.SourceUrl, image.Secret.OfflineUrl, overwrite
            );
        }
    }

    private async Task DownloadContentImages(
        IEnumerable<ChildNote> childNotes, bool overwrite
    )
    {
        var images = childNotes
            .SelectMany(childNote => childNote.Images)
            .OrderBy(image => image.Secret.OfflineUrl)
            .ToList();
        Console.WriteLine("downloading child note content images...");
        foreach (var image in images)
        {
            await this.DownloadHttpResource(
                image.Secret.SourceUrl, image.Secret.OfflineUrl, overwrite
            );
        }
    }

}
