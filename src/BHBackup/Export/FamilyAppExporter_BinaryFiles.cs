using BHBackup.Client.ApiV1.Feeds.Models;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private async Task DownloadBinaryFiles(
        IEnumerable<FeedItem> feedItems,
        bool overwrite
    )
    {
        Console.WriteLine("downloading feed item files...");
        var feedFiles = feedItems
            .SelectMany(feedItem => feedItem.Files)
            .OrderBy(feedImage => feedImage.OfflineUrl)
            .ToList();
        Console.WriteLine("downloading feed item content files...");
        foreach (var feedFile in feedFiles)
        {
            await this.DownloadHttpResource(
                feedFile.Url, feedFile.OfflineUrl, overwrite
            );
        }
    }

}