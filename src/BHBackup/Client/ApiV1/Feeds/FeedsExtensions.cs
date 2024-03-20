using BHBackup.Client.ApiV1.Feeds.Api;

namespace BHBackup.Client.ApiV1.Feeds;

internal static class FeedsExtensions
{

    public static async Task<GetFeedsResponse> GetFeedItems(
        this ApiV1Client apiV1Client,
        DateTime? olderThan, int targetHeight = 1331
    )
    {
        var querystring = new Dictionary<string, string>();
        if (olderThan is not null)
        {
            querystring.Add("olderThan", olderThan.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss+00:00"));
        }
        querystring.Add("targetHeight", targetHeight.ToString());

        return await apiV1Client.ExecuteApiV1Request<GetFeedsResponse>(
            requestUrl: "api/feed/feed/feed",
            querystring: querystring,
            method: HttpMethod.Get,
            requestBody: null,
            roundtrip: true
        ) ?? throw new InvalidOperationException();
    }

    public static async IAsyncEnumerable<GetFeedsResponse> PaginateFeedItems(
        this ApiV1Client apiV1Client,
        Action<DateTime>? onBeforeRequest = null
    )
    {
        ArgumentNullException.ThrowIfNull(apiV1Client);
        var timestamp = DateTime.UtcNow;
        while (true)
        {
            onBeforeRequest?.Invoke(timestamp);
            var responseObject = await apiV1Client.GetFeedItems(timestamp)
                ?? throw new InvalidOperationException();
            if (responseObject.FeedItems.Count == 0)
            {
                yield break;
            }
            yield return responseObject;
            timestamp = responseObject.FeedItems.Last().ParseCreatedDate();
        }
    }

}
