using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Authentication;

internal sealed class FailedInvalidPassword : AuthenticateWithPassword
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

    [JsonPropertyName("errorDetails")]
    [JsonPropertyOrder(3)]
    public string ErrorDetails
    {
        get;
        init;
    }

    [JsonPropertyName("errorTitle")]
    [JsonPropertyOrder(4)]
    public string ErrorTitle
    {
        get;
        init;
    }

}
