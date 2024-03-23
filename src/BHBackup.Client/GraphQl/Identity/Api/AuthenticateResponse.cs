using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Api;

public sealed class AuthenticateResponse
{

    [JsonPropertyName("data")]
    public AuthenticateData Data
    {
        get;
        set;
    }

}
