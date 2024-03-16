using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class AssessmentSettings
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
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

    [JsonPropertyName("title")]
    public string Title
    {
        get;
        init;
    }

}
