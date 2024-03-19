using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.LearningJourney.Models;

internal sealed class Area3
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

    [JsonPropertyName("title")]
    public string Title
    {
        get;
        init;
    }

    [JsonPropertyName("lower")]
    public int Lower
    {
        get;
        init;
    }

    [JsonPropertyName("upper")]
    public int Upper
    {
        get;
        init;
    }

}
