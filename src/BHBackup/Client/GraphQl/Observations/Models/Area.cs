using System.Text.Json.Serialization;

namespace BHBackup.Client.GraphQl.Observations.Models;

internal sealed class Area
{

    [JsonPropertyName("__typename")]
    [JsonPropertyOrder(99)]
    public string TypeName
    {
        get;
        init;
    }

    [JsonPropertyName("frameworkId")]
    public string FrameworkId
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

    [JsonPropertyName("parentId")]
    public object ParentId
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

    [JsonPropertyName("description")]
    public string Description
    {
        get;
        init;
    }

    /// <summary>
    /// Known values:
    ///   * "FF"   - "Feelings and Friendships"
    ///   * "TC"   - "Thinking Creatively"
    ///   * "STI"  - "Sharing Thoughts and Ideas"
    ///   * "TLS"  - "Technical and Life Skills"
    ///   * "ELAN" - "Exploring and Learning about my World"
    /// </summary>
    [JsonPropertyName("abbr")]
    public string Abbreviation
    {
        get;
        init;
    }

    [JsonPropertyName("color")]
    public int Color
    {
        get;
        init;
    }

    [JsonPropertyName("placement")]
    public int Placement
    {
        get;
        init;
    }

    [JsonPropertyName("deletedAt")]
    public object DeletedAt
    {
        get;
        init;
    }

    [JsonPropertyName("framework")]
    public Framework Framework
    {
        get;
        init;
    }

}
