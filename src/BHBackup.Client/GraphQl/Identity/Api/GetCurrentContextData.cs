using BHBackup.Client.GraphQl.Identity.Models.Context;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Api;

public sealed class GetCurrentContextData
{

    [JsonPropertyName("me")]
    public Me Me
    {
        get;
        set;
    }

}
