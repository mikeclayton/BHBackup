using BHDownload.Client.ApiV1.Feeds.Models;
using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV1.Feeds.Api;

internal sealed class GetFeedsResponse
{

    [JsonPropertyName("feedItems")]
    public List<FeedItem> FeedItems
    {
        get;
        init;
    }
}
