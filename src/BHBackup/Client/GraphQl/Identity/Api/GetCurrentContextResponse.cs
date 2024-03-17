using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Api;

internal sealed class GetCurrentContextResponse
{

    [JsonPropertyName("data")]
    public GetCurrentContextData Data
    {
        get;
        set;
    }

}
