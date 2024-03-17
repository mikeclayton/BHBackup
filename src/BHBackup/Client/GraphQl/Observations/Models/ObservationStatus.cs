using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class ObservationStatus
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    /// <remarks>
    /// Known values: "SENT"
    /// </remarks>
    [JsonPropertyName("state")]
    public string State
    {
        get;
        init;
    }

    [JsonPropertyName("createdAt")]
    public string CreatedAt
    {
        get;
        init;
    }

    [JsonIgnore()]
    public DateTime CreatedAtDateTime
    {
        get
        {
            return DateTime.Parse(this.CreatedAt);
        }
    }

}
