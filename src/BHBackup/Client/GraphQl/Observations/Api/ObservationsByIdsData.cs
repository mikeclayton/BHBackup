using BHBackup.Client.GraphQl.Observations.Models;
using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Api;

internal sealed class ObservationsByIdsData
{

    [JsonPropertyName("childDevelopment")]
    public ChildDevelopment ChildDevelopment
    {
        get;
        init;
    }

}
