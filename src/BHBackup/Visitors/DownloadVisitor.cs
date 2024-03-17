using BHBackup.Helpers;

namespace BHBackup.Visitors;

internal sealed partial class DownloadVisitor : RepositoryVisitor
{

    public DownloadVisitor(DownloadHelper downloadHelper)
    {
        this.DownloadHelper = downloadHelper ?? throw new ArgumentNullException(nameof(downloadHelper));
    }

    private DownloadHelper DownloadHelper
    {
        get;
    }

}
