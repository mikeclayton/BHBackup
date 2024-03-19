using BHBackup.Client.ApiV2.Sidebar.Models;

namespace BHBackup.Visitors;

internal sealed partial class DownloadVisitor
{

    public override void Visit(Sidebar sidebar)
    {
        var childItems = sidebar.ChildProfileItems
            .DistinctBy(item => item.Icon)
            .ToList();
        Console.WriteLine("downloading sidebar profile images...");
        foreach (var childItem in childItems)
        {
            this.Visit(childItem);
        }
    }

    public override void Visit(SidebarItem item)
    {
        this.DownloadHelper.DownloadHttpResource(
            item.Icon, item.OfflineIcon
        ).GetAwaiter().GetResult();
    }

}
