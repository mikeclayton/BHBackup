using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

/// <summary>
/// Same as ChildDevelopmentAreaProgress1, but with different json serialization order
/// </summary>
public sealed class ChildDevelopmentAreaProgress2
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("area")]
    public Area2 Area
    {
        get;
        init;
    }

    [JsonPropertyName("progressAssessment")]
    public string ProgressAssessmentArea
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

    [JsonPropertyName("lowestUncompletedAgeBands")]
    public List<ChildDevelopmentAreaProgress3> LowestUncompletedAgeBands
    {
        get;
        init;
    }

}
