using System.Text.Json.Serialization;
using BHDownload.Client.GraphQl.Identity.Models;

namespace BHDownload.Client.GraphQl.Identity.Api;

internal sealed class GetCurrentContextData
{

    [JsonPropertyName("me")]
    public Me Me
    {
        get;
        set;
    }

}
