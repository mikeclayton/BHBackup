using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Context;

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
