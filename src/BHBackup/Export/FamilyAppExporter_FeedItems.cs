using BHBackup.Client.ApiV1.Feeds;
using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Helpers;
using System.Collections.ObjectModel;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private IEnumerable<FeedItem> DownloadFeedItemData()
    {
        var feedsClient = this.DownloadHelper.GetApiV1Client();
        // read the feed items from the api
        Console.WriteLine("downloading feed item data...");
        var feedItems = feedsClient.PaginateFeedItems(
                onBeforeReadPage: timestamp =>
                    Console.WriteLine($"    downloading feed item data from {timestamp:yyyy-MM-dd}")
            ).ToBlockingEnumerable()
            .SelectMany(
                response => response.FeedItems
            ).ToList();
        // save the feed items to disk in individual files
        foreach (var feedItem in feedItems)
        {
            this.WriteRepositoryJsonFile(
                OfflinePathHelper.GetFeedItemDataFileRelativePath(feedItem.FeedItemId),
                feedItem
            );
            yield return feedItem;
        }
    }

    private ReadOnlyCollection<FeedItem> ReadFeedItemData(bool roundtrip)
    {
        Console.WriteLine("reading cached feed items...");
        var cacheFiles = this.GetRepositoryFiles(
            OfflinePathHelper.GetFeedItemDataFileRootPath(),
            "feeditem-*.json"
        );
        var feedItems = cacheFiles.Select(
               cacheFile => this.ReadRepositoryJsonFile<FeedItem>(cacheFile, roundtrip, true)
            ).OrderByDescending(feedItem => feedItem.ParseCreatedDate())
            .ToList()
            .AsReadOnly();
        return feedItems;
    }

}