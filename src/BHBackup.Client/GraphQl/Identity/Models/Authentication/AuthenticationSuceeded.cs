using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Authentication;

public sealed class AuthenticationSucceeded : AuthenticateWithPassword
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(2)]
    public new string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("status")]
    [JsonPropertyOrder(1)]
    public new string Status
    {
        get;
        init;
    }

    [JsonPropertyName("accessToken")]
    [JsonPropertyOrder(3)]
    public string AccessToken
    {
        get;
        init;
    }

    [JsonPropertyName("deviceId")]
    [JsonPropertyOrder(4)]
    public string DeviceId
    {
        get;
        init;
    }

}
