using BHBackup.Client.ApiV1;
using BHBackup.Client.ApiV2;
using BHBackup.Client.Core;
using BHBackup.Client.GraphQl;

namespace BHBackup.Helpers;

internal sealed class DownloadHelper
{

    public DownloadHelper(string repositoryDirectory, HttpClient httpClient, CoreApiCredentials apiCredentials, bool overwrite)
    {
        this.RepositoryDirectory = repositoryDirectory ?? throw new ArgumentNullException(nameof(repositoryDirectory));
        this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.ApiCredentials = apiCredentials ?? throw new ArgumentNullException(nameof(apiCredentials));
        this.Overwrite = overwrite;
    }

    public string RepositoryDirectory { get; }

    public HttpClient HttpClient { get; }

    public CoreApiCredentials ApiCredentials { get; }

    public bool Overwrite { get;  }

    public ApiV1Client GetApiV1Client()
    {
        return new ApiV1Client(
            this.HttpClient,
            this.ApiCredentials
        );
    }

    public ApiV2Client GetApiV2Client()
    {
        return new ApiV2Client(
            this.HttpClient,
            this.ApiCredentials
        );
    }

    public GraphQlClient GetGraphQlClient()
    {
        return new GraphQlClient(
            this.HttpClient,
            this.ApiCredentials
        );
    }

    public string GetAbsoluteFilename(string relativePath)
    {
        return Path.Join(this.RepositoryDirectory, relativePath);
    }

    public async Task DownloadHttpResource(string resourceUri, string relativePath)
    {
        ArgumentNullException.ThrowIfNull(resourceUri);
        ArgumentNullException.ThrowIfNull(relativePath);
        // overwrite existing file?
        var absolutePath = this.GetAbsoluteFilename(relativePath);
        if (!this.Overwrite && File.Exists(absolutePath))
        {
            Console.WriteLine($"    skipping '{relativePath}'...");
            return;
        }
        // create the destination directory if it doesn't already exist
        Directory.CreateDirectory(
            Path.GetDirectoryName(absolutePath) ?? throw new InvalidOperationException()
        );
        // build the request message
        var request = new HttpRequestMessage(HttpMethod.Get, resourceUri);
        request.Headers.ConnectionClose = false;
        // download the resource
        Console.WriteLine($"    downloading '{relativePath}'...");
        var response = await this.HttpClient.SendAsync(request);
        await using var responseStream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write);
        await responseStream.CopyToAsync(fileStream);
    }

}
