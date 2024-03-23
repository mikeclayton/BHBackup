using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV1.Feeds.Models;

public sealed class FeedFile
{

    [JsonPropertyName("fileId")]
    public string FileId
    {
        get;
        init;
    }

    [JsonPropertyName("url")]
    public string Url
    {
        get;
        init;
    }

    [JsonPropertyName("filename")]
    public string Filename
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
