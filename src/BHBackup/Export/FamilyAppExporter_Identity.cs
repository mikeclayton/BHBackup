using BHBackup.Client.Core;
using BHBackup.Client.GraphQl;
using BHBackup.Client.GraphQl.Identity.Api;
using BHBackup.Helpers;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private GetCurrentContextResponse DownloadCurrentContext()
    {

        var graphQlClient = new GraphQlClient(
            this.HttpClient,
            () => LoginHelpers.Authenticate(
                this.HttpClient,
                this.Username,
                this.Password,
                this.DeviceId
            ).Result
        );

        Console.WriteLine("downloading identity...");
        var currentContext = graphQlClient.GetCurrentContext().Result;

        // save the current context to disk
        this.WriteRepositoryJsonFile(
            OfflinePathHelper.GetCurrentContextDataFileRelativePath(),
            currentContext
        );

        return currentContext;

    }

    private GetCurrentContextResponse? ReadCurrentContext(bool roundtrip)
    {

        Console.WriteLine("reading cached identity...");
        var currentContext = this.ReadRepositoryJsonFile<GetCurrentContextResponse>(
            OfflinePathHelper.GetCurrentContextDataFileRelativePath(),
            roundtrip
        );
        return currentContext;
    }

}