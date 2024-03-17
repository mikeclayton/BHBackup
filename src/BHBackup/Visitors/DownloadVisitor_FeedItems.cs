using BHBackup.Client.ApiV1.Feeds.Models;

namespace BHBackup.Visitors;

internal sealed partial class DownloadVisitor
{

    public override void Visit(IEnumerable<FeedItem> feedItems)
    {
        var feedItemList = feedItems.ToList();
        base.Visit(feedItemList);
        // feed items - profile images
        Console.WriteLine("downloading feed item profiles");
        var senders = feedItemList
            .Select(feedItem => feedItem.Sender)
            .DistinctBy(sender => sender.OfflineUrl);
        foreach (var sender in senders)
        {
            this.DownloadHelper.DownloadHttpResource(
                sender.ProfileImage, sender.OfflineUrl
            ).GetAwaiter().GetResult();
        }
        // feed items - content files
        Console.WriteLine("downloading feed item content files");
        var feedFiles = feedItemList.SelectMany(
            feedItem => feedItem.Files
        );
        foreach (var feedFile in feedFiles)
        {
            this.DownloadHelper.DownloadHttpResource(
                feedFile.Url, feedFile.OfflineUrl
            ).GetAwaiter().GetResult();
        }
        // feed items - content images
        Console.WriteLine("downloading feed item content images");
        var feedImages = feedItemList.SelectMany(
            feedItem => feedItem.Images
        );
        foreach (var feedImage in feedImages)
        {
            this.DownloadHelper.DownloadHttpResource(
                feedImage.FullSizeUrl, feedImage.OfflineUrl
            ).GetAwaiter().GetResult();
        }
    }

}
