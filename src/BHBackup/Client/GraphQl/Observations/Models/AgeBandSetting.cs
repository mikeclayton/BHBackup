using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class AgeBandSetting
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("ageBandSettingId")]
    public string AgeBandSettingId
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

    [JsonPropertyName("from")]
    public int From
    {
        get;
        init;
    }

    [JsonPropertyName("to")]
    public int To
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