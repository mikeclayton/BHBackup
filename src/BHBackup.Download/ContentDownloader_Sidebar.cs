using BHBackup.Client.ApiV2.Sidebar.Models;
using BHBackup.Storage.Repositories;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public Sidebar DownloadSidebarData(SidebarRepository repository)
    {
        var apiV2Client = this.GetApiV2Client();
        Console.WriteLine("downloading sidebar...");
        var sidebar = apiV2Client.GetSidebar().Result;
        // save the context to disk
        repository.WriteItem(sidebar);
        return sidebar;
    }


    public  void DownloadSidebarContent(Sidebar sidebar)
    {
        var childItems = sidebar.ChildProfileItems
            .DistinctBy(item => item.Icon)
            .ToList();
        Console.WriteLine("downloading sidebar profile images...");
        foreach (var childItem in childItems)
        {
            this.DownloadHttpResource(
                childItem.Icon, childItem.OfflineIcon
            ).GetAwaiter().GetResult();
        }
    }

}
