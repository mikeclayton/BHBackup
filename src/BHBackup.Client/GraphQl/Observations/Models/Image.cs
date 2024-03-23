using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

public sealed class Image
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

    [JsonIgnore]
    public string FullSizeUrl
        => $"{this.Secret.Prefix}/{this.Secret.Key}/{this.Width}x{this.Height}/{this.Secret.Path}?expires={this.Secret.Expires}";

    [JsonIgnore]
    public string OfflineUrl
    {
        get;
        set;
    }

}
