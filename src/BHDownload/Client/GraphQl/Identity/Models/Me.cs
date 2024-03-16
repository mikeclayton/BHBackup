using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Identity.Models;

internal sealed class Me
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("currentContext")]
    public UserContext CurrentContext
    {
        get;
        init;
    }

    [JsonPropertyName("availableContexts")]
    public List<UserContext> AvailableContexts
    {
        get;
        init;
    }

}
