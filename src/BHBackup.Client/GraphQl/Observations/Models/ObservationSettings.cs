using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

public sealed class ObservationSettings
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
