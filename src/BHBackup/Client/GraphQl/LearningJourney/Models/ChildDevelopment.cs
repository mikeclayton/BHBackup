using BHBackup.Client.GraphQl.Observations.Models;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

internal sealed class ChildDevelopment
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("progress")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ChildDevelopmentProgress> Progress
    {
        get;
        set;
    }

    [JsonPropertyName("behaviors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<object> Behaviors
    {
        get;
        set;
    }

    [JsonPropertyName("observations")]
    public ObservationResult Observations
    {
        get;
        init;
    }

}
