using BHBackup.Client.GraphQl.Observations.Models;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

/// <summary>
/// Same as an ObservationResult but with slightly different serialization atributes
/// </summary>
public sealed class LearningJourneyObservationResult
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
    public string Next
    {
        get;
        init;
    }

}
