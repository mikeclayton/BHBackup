using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

public sealed class ChildDevelopmentAreaRefinementSetting
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("ageBandSetting")]
    public AgeBandSetting AgeSetting
    {
        get;
        init;
    }

    [JsonPropertyName("assessmentOptionSetting")]
    public AssessmentOptionSetting AssessmentOptionSetting
    {
        get;
        init;
    }

}
