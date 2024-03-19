using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

internal class ChildDevelopmentAgeBand
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("completion")]
    public object Completion
    {
        get;
        init;
    }

}
