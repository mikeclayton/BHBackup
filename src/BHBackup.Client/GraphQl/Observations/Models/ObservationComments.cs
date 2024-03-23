using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

public sealed class ObservationComments
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
