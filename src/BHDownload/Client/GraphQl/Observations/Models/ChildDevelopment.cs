using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class ChildDevelopment
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("observations")]
    public ObservationResult Observations
    {
        get;
        init;
    }

}
