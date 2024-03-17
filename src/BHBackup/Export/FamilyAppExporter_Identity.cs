using BHBackup.Client.GraphQl.Identity.Api;
using BHBackup.Helpers;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private GetCurrentContextResponse DownloadCurrentContextData()
    {
        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
        Console.WriteLine("downloading identity...");
        var currentContext = graphQlClient.GetCurrentContext().Result;
        // save the current context to disk
        this.WriteRepositoryJsonFile(
            OfflinePathHelper.GetCurrentContextDataFileRelativePath(),
            currentContext
        );
        return currentContext;
    }

    private GetCurrentContextResponse? ReadCurrentContextData(bool roundtrip)
    {
        Console.WriteLine("reading cached identity...");
        var currentContext = this.ReadRepositoryJsonFile<GetCurrentContextResponse>(
            OfflinePathHelper.GetCurrentContextDataFileRelativePath(),
            roundtrip
        );
        return currentContext;
    }

}