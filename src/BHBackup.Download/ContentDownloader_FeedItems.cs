using BHBackup.Client.ApiV1.Feeds;
using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Download.Extensions;
using BHBackup.Storage.Repositories;
using Microsoft.Extensions.Logging;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public async IAsyncEnumerable<FeedItem> DownloadFeedItemData(FeedItemRepository repository)
    {
        var feedsClient = this.GetApiV1Client();
        // read the feed items from the api
        this.Logger.LogInformation("downloading feed item data...");
        var responses = await feedsClient.PaginateFeedItems(
                onBeforeRequest: timestamp =>
                    this.Logger.LogInformation($"downloading feed item data from {timestamp:yyyy-MM-dd}")
            ).ConfigureAwait(false).ToListAsync();
        var feedItems = responses
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

    public async Task DownloadFeedItemContent(IEnumerable<FeedItem> feedItems)
    {
        var feedItemList = feedItems.ToList();
        // feed items - profile images
        this.Logger.LogInformation("downloading feed item profiles");
        var senders = feedItemList
            .Select(feedItem => feedItem.Sender)
            .DistinctBy(sender => sender.OfflineUrl);
        foreach (var sender in senders)
        {
            await this.DownloadHttpResource(
                sender.ProfileImage, sender.OfflineUrl
            ).ConfigureAwait(false);
        }
        // feed items - content files
        this.Logger.LogInformation("downloading feed item content files");
        var feedFiles = feedItemList.SelectMany(
            feedItem => feedItem.Files
        );
        foreach (var feedFile in feedFiles)
        {
            await this.DownloadHttpResource(
                feedFile.Url, feedFile.OfflineUrl
            ).ConfigureAwait(false);
        }
        // feed items - content images
        this.Logger.LogInformation("downloading feed item content images");
        var feedImages = feedItemList.SelectMany(
            feedItem => feedItem.Images
        );
        foreach (var feedImage in feedImages)
        {
            await this.DownloadHttpResource(
                feedImage.FullSizeUrl, feedImage.OfflineUrl
            ).ConfigureAwait(false);
        }
    }


}
