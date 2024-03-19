using BHBackup.Client.GraphQl.Identity.Models.Authentication;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Api;

internal sealed class AuthenticateData
{

    [JsonPropertyName("me")]
    public MeMutation Me
    {
        get;
        set;
    }

}
