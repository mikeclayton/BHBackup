using BHBackup.Client.ApiV2.Sidebar.Models;
using BHBackup.Storage.Repositories;
using Microsoft.Extensions.Logging;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public async Task<Sidebar> DownloadSidebarData(SidebarRepository repository)
    {
        var apiV2Client = this.GetApiV2Client();
        this.Logger.LogInformation("downloading sidebar...");
        var sidebar = await apiV2Client.GetSidebar().ConfigureAwait(false);
        // save the context to disk
        repository.WriteItem(sidebar);
        return sidebar;
    }

    public async Task DownloadSidebarContent(Sidebar sidebar)
    {
        var childItems = sidebar.ChildProfileItems
            .DistinctBy(item => item.Icon)
            .ToList();
        this.Logger.LogInformation("downloading sidebar profile images...");
        foreach (var childItem in childItems)
        {
            await this.DownloadHttpResource(
                childItem.Icon, childItem.OfflineIcon
            ).ConfigureAwait(false);
        }
    }

}
