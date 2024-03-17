using BHBackup.Client.ApiV2.Models;
using BHBackup.Helpers;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private async IAsyncEnumerable<ChildSummary> DownloadChildSummaryData(Sidebar sidebar)
    {
        var apiV2Client = this.DownloadHelper.GetApiV2Client();
        // save the child summaries to disk in individual files
        Console.WriteLine("downloading child summaries...");
        foreach (var item in sidebar.ChildProfileItems)
        {
            var childSummary = await apiV2Client.GetChildSummary(item.Id);
            this.WriteRepositoryJsonFile(
                OfflinePathHelper.GetChildSummaryDataFileRelativePath(item.Id),
                childSummary
            );
            yield return childSummary;
        }
    }

    private IEnumerable<ChildSummary> ReadChildSummaryData(bool roundtrip)
    {
        Console.WriteLine("reading cached child summaries...");
        var cacheFiles = this.GetRepositoryFiles(
            OfflinePathHelper.GetChildSummaryDataFileRootPath(),
            "childsummary-*.json"
        );
        var childSummaries = cacheFiles.Select(
                cacheFile => this.ReadRepositoryJsonFile<ChildSummary>(cacheFile, roundtrip, true)
            ).ToList()
            .AsReadOnly();
        return childSummaries;
    }

}