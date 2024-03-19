using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV2.ChildSummary.Models;

internal sealed class ChildSummary
{

    [JsonPropertyName("institution")]
    public SummaryInstitution Institution
    {
        get;
        init;
    }

    [JsonPropertyName("child")]
    public SummaryChild Child
    {
        get;
        init;
    }

    [JsonPropertyName("group")]
    public SummaryGroup Group
    {
        get;
        init;
    }

    [JsonPropertyName("tags")]
    public List<object> Tags
    {
        get;
        init;
    }

    [JsonPropertyName("relations")]
    public List<object> Relations
    {
        get;
        init;
    }

    [JsonPropertyName("naps")]
    public List<SummaryNap> Naps
    {
        get;
        init;
    }

    [JsonPropertyName("permissions")]
    public object Permissions
    {
        get;
        init;
    }

}
