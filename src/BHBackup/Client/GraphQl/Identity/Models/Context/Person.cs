using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Context;

internal sealed class Person : IPerson
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("name")]
    public Name Name
    {
        get;
        init;
    }

    [JsonPropertyName("profileImage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ProfileImage? ProfileImage
    {
        get;
        init;
    }

}
