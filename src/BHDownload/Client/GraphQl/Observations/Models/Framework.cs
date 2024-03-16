using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class Framework
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("id")]
    public string Id
    {
        get;
        init;
    }

    [JsonPropertyName("title")]
    public string Title
    {
        get;
        init;
    }

    [JsonPropertyName("owner")]
    public object Owner
    {
        get;
        init;
    }

}
