using BHBackup.Client.ApiV2.Models;
using BHBackup.Helpers;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private Sidebar DownloadSidebarData()
    {
        var apiV2Client = this.DownloadHelper.GetApiV2Client();
        Console.WriteLine("downloading sidebar...");
        var sidebar = apiV2Client.GetSidebar().Result;
        // save the context to disk
        this.WriteRepositoryJsonFile(
            OfflinePathHelper.GetSidebarDataFileRelativePath(),
            sidebar
        );
        return sidebar;
    }

    private Sidebar ReadSidebarData(bool roundtrip)
    {
        Console.WriteLine("reading cached sidebar...");
        return this.ReadRepositoryJsonFile<Sidebar>(
            OfflinePathHelper.GetSidebarDataFileRelativePath(), 
            roundtrip
        );
    }

}