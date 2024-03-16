using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Identity.Models;

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
