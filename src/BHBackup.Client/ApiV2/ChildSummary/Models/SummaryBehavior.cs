using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV2.ChildSummary.Models;

public sealed class SummaryBehavior
{

    [JsonPropertyName("id")]
    public string Id
    {
        get;
        init;
    }

    [JsonPropertyName("payload")]
    public object Payload
    {
        get;
        init;
    }

}
