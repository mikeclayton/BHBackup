using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class AssessmentOptionSetting
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("assessmentOptionSettingId")]
    public string AssessmentOptionSettingId
    {
        get;
        init;
    }

    [JsonPropertyName("id")]
    public string Id
    {
        get;
        init;
    }

    [JsonPropertyName("assessmentSettingsId")]
    public string AssessmentSettingsId
    {
        get;
        init;
    }

    [JsonPropertyName("backgroundColor")]
    public int BackgroundColor
    {
        get;
        init;
    }

    [JsonPropertyName("fontColor")]
    public int FontColor
    {
        get;
        init;
    }

    [JsonPropertyName("label")]
    public string Label
    {
        get;
        init;
    }

}
