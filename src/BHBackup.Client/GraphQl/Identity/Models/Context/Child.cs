using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Context;

public sealed class Child
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
    public ProfileImage ProfileImage
    {
        get;
        init;
    }

}
