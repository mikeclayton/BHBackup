using System.Text.Json.Serialization;
using BHBackup.Client.GraphQl.Identity.Models;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class ObservationPerson : IPerson
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

    /// <summary>
    /// Same as an Identity.Person, except it includes '"profileImage": null' in the json representation
    /// </summary>
    [JsonPropertyName("profileImage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public ProfileImage? ProfileImage
    {
        get;
        init;
    }

}
