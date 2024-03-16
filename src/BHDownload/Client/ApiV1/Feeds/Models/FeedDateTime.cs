using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV1.Feeds.Models;

internal sealed class FeedDateTime
{

    [JsonPropertyName("date")]
    public string Date
    {
        get;
        init;
    }

    [JsonPropertyName("timezone_type")]
    public int TimezoneType
    {
        get;
        init;
    }

    [JsonPropertyName("timezone")]
    public string Timezone
    {
        get;
        init;
    }

}
