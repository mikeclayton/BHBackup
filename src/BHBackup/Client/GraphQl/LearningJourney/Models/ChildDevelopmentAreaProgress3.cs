using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

internal sealed class ChildDevelopmentAreaProgress3
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(3)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("area")]
    [JsonPropertyOrder(1)]
    public Area3 Area
    {
        get;
        init;
    }

    [JsonPropertyName("latestLink")]
    [JsonPropertyOrder(2)]
    public object LatestLink
    {
        get;
        init;
    }

    [JsonPropertyName("completion")]
    [JsonPropertyOrder(4)]
    public object Completion
    {
        get;
        init;
    }

}
