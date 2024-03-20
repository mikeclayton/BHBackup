using BHBackup.Client.ApiV1.Feeds.Models;

namespace BHBackup.Storage;

internal sealed class FeedItemRepository : OfflineRepository<FeedItem>
{

    public FeedItemRepository(string rootFolder, bool roundtrip)
        : base(rootFolder, roundtrip)
    {
    }

    private static string GetFeedItemFileRootPath()
    {
        return Path.Join(
            "data", "feeditems"
        );
    }

    private static string GetFeedItemFileRelativePath(string feedItemId)
    {
        return Path.Join(
            FeedItemRepository.GetFeedItemFileRootPath(),
            $"feeditem-{feedItemId}.json"
        );
    }

    #region OfflineRepository Interface

    public override IEnumerable<FeedItem> ReadAll()
    {
        Console.WriteLine("reading cached feed items...");
        var cacheFiles = base.GetRepositoryFiles(
            FeedItemRepository.GetFeedItemFileRootPath(),
            "feeditem-*.json"
        );
        var feedItems = cacheFiles.Select(
               cacheFile => base.ReadRepositoryJsonFile(cacheFile, true)
            ).OrderByDescending(feedItem => feedItem.ParseCreatedDate());
        return feedItems;
    }

    public override FeedItem ReadItem(string id)
    {
        return base.ReadRepositoryJsonFile(
            FeedItemRepository.GetFeedItemFileRelativePath(id)
        );
    }

    public override void WriteItem(FeedItem item)
    {
        base.WriteRepositoryJsonFile(
            FeedItemRepository.GetFeedItemFileRelativePath(item.FeedItemId),
            item
        );
    }

    #endregion

}
