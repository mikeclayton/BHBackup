using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Api;

public sealed class GetCurrentContextResponse
{

    [JsonPropertyName("data")]
    public GetCurrentContextData Data
    {
        get;
        set;
    }

}
