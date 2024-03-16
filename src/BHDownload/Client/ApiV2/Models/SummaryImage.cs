using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV2.Models;

internal sealed class SummaryImage
{

    [JsonPropertyName("large")]
    public string Large
    {
        get;
        init;
    }

    [JsonPropertyName("small")]
    public string Small
    {
        get;
        init;
    }

    [JsonPropertyName("imageId")]
    public string ImageId
    {
        get;
        init;
    }

    [JsonPropertyName("isEmpty")]
    public bool IsEmpty
    {
        get;
        init;
    }

    [JsonPropertyName("colorCode")]
    public int ColorCode
    {
        get;
        init;
    }

    [JsonIgnore]
    public string OfflineUrl
    {
        get;
        set;
    }

}
