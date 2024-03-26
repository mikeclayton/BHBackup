using BHBackup.Client.ApiV2.ChildSummary;
using BHBackup.Client.ApiV2.ChildSummary.Models;
using BHBackup.Storage.Repositories;
using Microsoft.Extensions.Logging;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public async IAsyncEnumerable<ChildSummary> DownloadChildSummaryData(ChildSummaryRepository repository, IEnumerable<string> childIds)
    {
        var apiV2Client = this.GetApiV2Client();
        // save the child summaries to disk in individual files
        this.Logger.LogInformation("downloading child summaries...");
        foreach (var childId in childIds)
        {
            var childSummary = await apiV2Client.GetChildSummary(childId).ConfigureAwait(false);
            repository.WriteItem(childSummary);
            yield return childSummary;
        }
    }

}
