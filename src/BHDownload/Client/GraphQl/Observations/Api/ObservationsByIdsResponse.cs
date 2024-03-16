using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Observations.Api;

internal sealed class ObservationsByIdsResponse
{

    [JsonPropertyName("data")]
    public ObservationsByIdsData Data
    {
        get;
        set;
    }
}
