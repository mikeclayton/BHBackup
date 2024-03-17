using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Api;

internal sealed class ObservationsByIdsResponse
{

    [JsonPropertyName("data")]
    public ObservationsByIdsData Data
    {
        get;
        set;
    }
}
