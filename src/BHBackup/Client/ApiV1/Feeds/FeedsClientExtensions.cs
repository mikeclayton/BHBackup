using BHBackup.Client.ApiV1.Feeds.Api;

namespace BHBackup.Client.ApiV1.Feeds;

internal static class FeedsClientExtensions
{

    public static async IAsyncEnumerable<GetFeedsResponse> PaginateFeedItems(
        this ApiV1Client apiV1Client,
        Action<DateTime>? onBeforeReadPage = null
    )
    {

        ArgumentNullException.ThrowIfNull(apiV1Client);

        var timestamp = DateTime.UtcNow;
        while (true)
        {
            onBeforeReadPage?.Invoke(timestamp);
            var responseObject = await apiV1Client.GetFeedItems(timestamp)
                ?? throw new InvalidOperationException();
            if (responseObject.FeedItems.Count == 0)
            {
                break;
            }
            yield return responseObject;
            timestamp = responseObject.FeedItems.Last().ParseCreatedDate();
        }

    }

}
