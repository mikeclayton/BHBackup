using BHBackup.Client.ApiV2;
using BHBackup.Client.ApiV2.Models;
using BHBackup.Client.Core;
using BHBackup.Helpers;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private async IAsyncEnumerable<ChildSummary> DownloadChildSummaries(Sidebar sidebar)
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


    private IEnumerable<ChildSummary> ReadChildSummaries(bool roundtrip)
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