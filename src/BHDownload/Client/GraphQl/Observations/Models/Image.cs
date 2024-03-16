using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class Image
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
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

    [JsonPropertyName("width")]
    public int Width
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

    [JsonPropertyName("secret")]
    public ImageSecret Secret
    {
        get;
        init;
    }

}
