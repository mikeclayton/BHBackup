using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Models;

internal sealed class ObservationSettings
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("assessmentSetting")]
    public AssessmentSettings AssessmentSetting
    {
        get;
        init;
    }

}
