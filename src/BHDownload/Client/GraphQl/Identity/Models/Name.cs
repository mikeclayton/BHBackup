using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Identity.Models;

internal sealed class Name
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("firstName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string FirstName
    {
        get;
        init;
    }

    [JsonPropertyName("fullName")]
    public string FullName
    {
        get;
        init;
    }

}
