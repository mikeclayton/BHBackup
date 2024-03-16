using System.Text.Json.Serialization;

namespace BHDownload.Client.ApiV2.Models;

internal sealed class Sidebar
{

    [JsonPropertyName("items")]
    public List<SidebarItem> Items
    {
        get;
        init;
    }

    [JsonPropertyName("default")]
    public SidebarItem Default
    {
        get;
        init;
    }

    [JsonPropertyName("behaviors")]
    public List<SidebarBehavior> Behaviors
    {
        get;
        init;
    }

    [JsonIgnore]
    public List<SidebarItem> ChildProfileItems
    {
        get
        {
            return this.Items
                .Where(item => item.Type == SidebarItem.ChildItemType)
                .ToList();
        }
    }

}
