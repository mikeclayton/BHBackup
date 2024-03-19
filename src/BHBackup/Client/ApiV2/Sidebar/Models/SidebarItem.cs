using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV2.Sidebar.Models;

internal sealed class SidebarItem
{

    public const string NewsfeedItemType = "Newsfeed";
    public const string ChildItemType = "Famly.Daycare:Child";

    [JsonPropertyName("id")]
    public string Id
    {
        get;
        init;
    }

    /// <summary>
    /// Known values: "newsfeed", "Family.Daycare:Child"
    /// </summary>
    [JsonPropertyName("type")]
    public string Type
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

    [JsonPropertyName("subtitle")]
    public object Subtitle
    {
        get;
        init;
    }


    [JsonPropertyName("icon")]
    public string Icon
    {
        get;
        init;
    }

    [JsonIgnore]
    public string OfflineIcon
    {
        get;
        set;
    }

    [JsonPropertyName("link")]
    public string Link
    {
        get;
        init;
    }

    [JsonIgnore]
    public string OfflineLink
    {
        get;
        set;
    }

    [JsonPropertyName("items")]
    public object Items
    {
        get;
        init;
    }

}
