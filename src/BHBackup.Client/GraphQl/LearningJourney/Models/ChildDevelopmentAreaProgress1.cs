using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

public sealed class ChildDevelopmentAreaProgress1
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("area")]
    public Area1 Area
    {
        get;
        init;
    }

    [JsonPropertyName("subAreaProgress")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<ChildDevelopmentAreaProgress2> SubAreaProgress
    {
        get;
        init;
    }

    [JsonPropertyName("ageBands")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<ChildDevelopmentAgeBand> AgeBands
    {
        get;
        init;
    }

}
