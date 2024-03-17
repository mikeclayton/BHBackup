using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class ImageSecret
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("crop")]
    public object Crop
    {
        get;
        init;
    }

    [JsonPropertyName("expires")]
    public string Expires
    {
        get;
        init;
    }

    [JsonPropertyName("key")]
    public string Key
    {
        get;
        init;
    }

    [JsonPropertyName("path")]
    public string Path
    {
        get;
        init;
    }

    [JsonPropertyName("prefix")]
    public string Prefix
    {
        get;
        init;
    }

}
