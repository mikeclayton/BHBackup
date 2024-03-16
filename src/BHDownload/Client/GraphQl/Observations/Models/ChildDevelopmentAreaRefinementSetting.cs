using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class ChildDevelopmentAreaRefinementSetting
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
