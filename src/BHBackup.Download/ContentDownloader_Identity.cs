using BHBackup.Client.GraphQl.Identity;
using BHBackup.Client.GraphQl.Identity.Api;
using BHBackup.Storage.Repositories;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public GetCurrentContextResponse DownloadCurrentContextData(IdentityRepository repository)
    {
        var graphQlClient = this.GetGraphQlClient();
        Console.WriteLine("downloading identity...");
        var currentContext = graphQlClient.GetCurrentContext().Result;
        // save the current context to disk
        repository.WriteItem(currentContext);
        return currentContext;
    }

}
