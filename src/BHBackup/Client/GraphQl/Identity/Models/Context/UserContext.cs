using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Context;

internal sealed class UserContext
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("id")]
    public string Id
    {
        get;
        init;
    }

    [JsonPropertyName("target")]
    public PersonContextTarget Target
    {
        get;
        init;
    }

}
