using System.Text.Json.Serialization;

namespace BHBackup.Client.ApiV2.Sidebar.Models;

internal sealed class SidebarBehavior
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
