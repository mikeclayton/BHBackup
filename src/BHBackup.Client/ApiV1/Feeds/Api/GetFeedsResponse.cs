using BHBackup.Client.ApiV1.Feeds.Models;
using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV1.Feeds.Api;

public sealed class GetFeedsResponse
{

    [JsonPropertyName("feedItems")]
    public List<FeedItem> FeedItems
    {
        get;
        init;
    }
}
