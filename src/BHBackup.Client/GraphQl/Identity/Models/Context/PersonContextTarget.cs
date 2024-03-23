using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Identity.Models.Context;

public sealed class PersonContextTarget
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(0)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("person")]
    public Person Person
    {
        get;
        init;
    }

    [JsonPropertyName("children")]
    public List<Child> Children
    {
        get;
        init;
    }

}
