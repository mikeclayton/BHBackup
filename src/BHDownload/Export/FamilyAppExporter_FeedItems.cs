using BHDownload.Client.ApiV1;
using BHDownload.Client.ApiV1.Feeds;
using BHDownload.Client.ApiV1.Feeds.Models;
using BHDownload.Client.Core;
using BHDownload.Helpers;
using System.Collections.ObjectModel;

namespace BHDownload.Export;

internal sealed partial class FamilyAppExporter
{

    private IEnumerable<FeedItem> DownloadFeedItems()
    {

        var feedsClient = new ApiV1Client(
            this.HttpClient,
            () => LoginHelpers.Authenticate(
                this.HttpClient,
                this.Username,
                this.Password,
                this.DeviceId
            ).Result
        );

        // read the feed items from the api
        Console.WriteLine("downloading feed items...");
        var feedItems = feedsClient.PaginateFeedItems(
                onBeforeReadPage: timestamp =>
                    Console.WriteLine($"    downloading feed items from {timestamp:yyyy-MM-dd}")
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

    private ReadOnlyCollection<FeedItem> ReadFeedItems(bool roundtrip)
    {
        Console.WriteLine("reading cached feed items...");
        var cacheFiles = this.GetRepositoryFiles(
            OfflinePathHelper.GetFeedItemDataFileRootPath(),
            "feeditem-*.json"
        );

        var feedItems = cacheFiles.Select(
               cacheFile => this.ReadRepositoryJsonFile<FeedItem>(cacheFile, false, true)
            ).OrderByDescending(feedItem => feedItem.ParseCreatedDate())
            .ToList()
            .AsReadOnly();

        return feedItems;
    }

}