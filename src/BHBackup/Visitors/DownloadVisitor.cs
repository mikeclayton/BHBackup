using BHBackup.Download;

namespace BHBackup.Visitors;

internal sealed partial class DownloadVisitor : RepositoryVisitor
{

    public DownloadVisitor(ContentDownloader downloader)
    {
        this.Downloader = downloader ?? throw new ArgumentNullException(nameof(downloader));
    }

    private ContentDownloader Downloader
    {
        get;
    }

}
