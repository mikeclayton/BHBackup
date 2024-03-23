using BHBackup.Client.GraphQl.Observations.Models;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.ChildNotes.Models;

public sealed class ChildNotesImage
{

    // basically an observation Image but properties serialized in a different order

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

    [JsonPropertyName("width")]
    public int Width
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
