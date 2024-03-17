using System.Text.Json.Serialization;
using BHBackup.Client.GraphQl.Identity.Models;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class ObservationFeedItem
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

}
