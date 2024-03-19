using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Authentication;

internal sealed class MeMutation
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("authenticateWithPassword")]
    public AuthenticateWithPassword AuthenticateWithPassword
    {
        get;
        init;
    }

}
