using System.Text.Json.Serialization;
using BHBackup.Client.GraphQl.Identity.Models;

namespace BHBackup.Client.GraphQl.Identity.Api;

internal sealed class GetCurrentContextData
{

    [JsonPropertyName("me")]
    public Me Me
    {
        get;
        set;
    }

}
