using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV1.Feeds.Models;

internal sealed class FeedImageInfo
{

    [JsonPropertyName("url")]
    public string Url
    {
        get;
        init;
    }

    [JsonPropertyName("width")]
    public int Width
    {
        get;
        init;
    }

    [JsonPropertyName("height")]
    public int Height
    {
        get;
        init;
    }

}
