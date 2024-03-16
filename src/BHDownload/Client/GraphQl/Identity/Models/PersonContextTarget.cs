using System.Text.Json.Serialization;

namespace BHDownload.Client.GraphQl.Identity.Models;

internal sealed class PersonContextTarget
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
