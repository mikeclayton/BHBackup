using BHDownload.Client.ApiV2;
using BHDownload.Client.ApiV2.Models;
using BHDownload.Client.Core;
using BHDownload.Helpers;

namespace BHDownload.Export;

internal sealed partial class FamilyAppExporter
{

    private async Task DownloadSidebarImages(
        Sidebar sidebar,
        bool overwrite
    )
    {
        var childItems = sidebar.ChildProfileItems
            .DistinctBy(item => item.Icon)
            .ToList();
        Console.WriteLine("downloading sidebar profile images...");
        foreach (var childItem in childItems)
        {
            await this.DownloadHttpResource(
                childItem.Icon, childItem.OfflineIcon, overwrite
            );
        }
    }

    private Sidebar DownloadSidebar()
    {

        var apiV2Client = new ApiV2Client(
            this.HttpClient,
            () => LoginHelpers.Authenticate(
                this.HttpClient,
                this.Username,
                this.Password,
                this.DeviceId
            ).Result
        );

        Console.WriteLine("downloading sidebar...");
        var sidebar  = apiV2Client.GetSidebar().Result;

        // save the context to disk
        this.WriteRepositoryJsonFile(
            OfflinePathHelper.GetSidebarDataFileRelativePath(),
            sidebar
        );

        return sidebar;

    }

    private Sidebar ReadSidebar(bool roundtrip)
    {
        Console.WriteLine("reading cached sidebar...");
        return this.ReadRepositoryJsonFile<Sidebar>(
            OfflinePathHelper.GetSidebarDataFileRelativePath(), 
            roundtrip
        );
    }

}