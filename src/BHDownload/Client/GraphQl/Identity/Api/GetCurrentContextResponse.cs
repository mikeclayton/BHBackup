using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Identity.Api;

internal sealed class GetCurrentContextResponse
{

    [JsonPropertyName("data")]
    public GetCurrentContextData Data
    {
        get;
        set;
    }

}
