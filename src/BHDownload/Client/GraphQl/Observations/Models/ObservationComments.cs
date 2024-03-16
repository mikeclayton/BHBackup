using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class ObservationComments
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("count")]
    public int Count
    {
        get;
        init;
    }

    [JsonPropertyName("results")]
    public List<object> Results
    {
        get;
        init;
    }

}
