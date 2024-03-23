using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

public sealed class ChildDevelopmentProgress
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("areaProgress")]
    public List<ChildDevelopmentAreaProgress1> AreaProgress
    {
        get;
        init;
    }

}
