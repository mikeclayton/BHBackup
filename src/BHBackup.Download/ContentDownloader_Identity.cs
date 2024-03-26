using BHBackup.Client.GraphQl.Identity;
using BHBackup.Client.GraphQl.Identity.Api;
using BHBackup.Storage.Repositories;
using Microsoft.Extensions.Logging;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public async Task<GetCurrentContextResponse> DownloadCurrentContextData(IdentityRepository repository)
    {
        var graphQlClient = this.GetGraphQlClient();
        this.Logger.LogInformation("downloading identity...");
        var currentContext = await graphQlClient.GetCurrentContext().ConfigureAwait(false);
        // save the current context to disk
        repository.WriteItem(currentContext);
        return currentContext;
    }

}
