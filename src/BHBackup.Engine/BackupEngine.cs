using BHBackup.Client.Core;
using BHBackup.Common.Helpers;
using BHBackup.Download;
using BHBackup.Render.Export;
using BHBackup.Storage;
using BHBackup.Storage.Helpers;
using BHBackup.Storage.Visitors;
using System.Diagnostics;

namespace BHBackup.Engine;

public static class BackupEngine
{

    public async static Task ExecuteBackup(BackupOptions options)
    {

        var repositoryFactory = new RepositoryFactory(
            rootFolder: options.OutputDirectory ?? throw new InvalidOperationException(),
            roundtrip: true
        );

        var repository = default(DataCollection);
        if (options.SkipDownload)
        {
            repository = DataCollection.ReadRepositoryData(repositoryFactory);
            new OfflineUrlVisitor().Visit(repository);
        }
        else
        {
            // use the anonymous "authenticate" endpoint to log in and get an api access token
            using var httpClient = new HttpClient();
            var apiCredentials = await LoginHelper.Authenticate(
                httpClient,
                username: options.Username ?? throw new InvalidOperationException(),
                password: options.Password ?? throw new InvalidOperationException(),
                deviceId: Guid.NewGuid().ToString()
            );
            // use the api access token to make any further api calls
            var downloader = new ContentDownloader(
                options.Logger,
                outputDirectory: options.OutputDirectory ?? throw new InvalidOperationException(),
                httpClient: httpClient,
                apiCredentials: apiCredentials,
                overwrite: false
            );
            // download the data, content and static resources
            repository = await downloader.DownloadRepositoryData(repositoryFactory).ConfigureAwait(false);
            new OfflineUrlVisitor().Visit(repository);
            await downloader.DownloadRepositoryContent(repository).ConfigureAwait(false);
            await downloader.DownloadStaticResources(repository).ConfigureAwait(false);
        }

        if (options.SkipGenerate && options.SkipLaunch)
        {
            return;
        }

        if (!options.SkipGenerate)
        {
            var htmlWriter = new HtmlWriter(options.OutputDirectory);
            htmlWriter.GenerateHtmlFiles(repository);
        }

        if (!options.SkipLaunch)
        {
            var indexPath = RelativePathMapper.GetAbsolutePath(
                options.OutputDirectory,
                OfflinePathHelper.GetIndexPageRelativePath()
            );
            _ = Process.Start(
                new ProcessStartInfo(indexPath)
                {
                    UseShellExecute = true
                }
            );
        }

    }

}
