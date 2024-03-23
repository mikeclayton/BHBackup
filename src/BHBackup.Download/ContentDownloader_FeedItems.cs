using BHBackup.Client.ApiV1.Feeds;
using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Storage.Repositories;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public IEnumerable<FeedItem> DownloadFeedItemData(FeedItemRepository repository)
    {
        var feedsClient = this.GetApiV1Client();
        // read the feed items from the api
        Console.WriteLine("downloading feed item data...");
        var feedItems = feedsClient.PaginateFeedItems(
                onBeforeRequest: timestamp =>
                    Console.WriteLine($"    downloading feed item data from {timestamp:yyyy-MM-dd}")
            ).ToBlockingEnumerable()
            .SelectMany(
                response => response.FeedItems
            ).ToList();
        // save the feed items to disk in individual files
        foreach (var feedItem in feedItems)
        {
            repository.WriteItem(feedItem);
            yield return feedItem;
        }
    }

    public void DownloadFeedItemContent(IEnumerable<FeedItem> feedItems)
    {
        var feedItemList = feedItems.ToList();
        // feed items - profile images
        Console.WriteLine("downloading feed item profiles");
        var senders = feedItemList
            .Select(feedItem => feedItem.Sender)
            .DistinctBy(sender => sender.OfflineUrl);
        foreach (var sender in senders)
        {
            this.DownloadHttpResource(
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
            this.DownloadHttpResource(
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
            this.DownloadHttpResource(
                feedImage.FullSizeUrl, feedImage.OfflineUrl
            ).GetAwaiter().GetResult();
        }
    }


}
