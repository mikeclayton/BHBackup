using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class ObservationResult
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("results")]
    public List<Observation> Results
    {
        get;
        init;
    }

    [JsonPropertyName("next")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Next
    {
        get;
        init;
    }

}
