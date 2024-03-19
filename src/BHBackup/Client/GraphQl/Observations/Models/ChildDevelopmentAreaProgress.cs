using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class ChildDevelopmentAreaProgress
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("area")]
    public Area Area
    {
        get;
        init;
    }

    [JsonPropertyName("subAreaProgress")]
    public List<ChildDevelopmentAreaProgress> SubAreaProgress
    {
        get;
        init;
    }

}
