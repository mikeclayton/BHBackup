using BHBackup.Client.ApiV1;
using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.ApiV2;
using BHBackup.Client.Core;
using BHBackup.Client.GraphQl;
using BHBackup.Common.Helpers;
using BHBackup.Storage;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public ContentDownloader(string outputDirectory, HttpClient httpClient, CoreApiCredentials apiCredentials, bool overwrite)
    {
        this.OutputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
        this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.ApiCredentials = apiCredentials ?? throw new ArgumentNullException(nameof(apiCredentials));
        this.Overwrite = overwrite;
    }

    public string OutputDirectory { get; }

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
        return RelativePathMapper.GetAbsolutePath(this.OutputDirectory, relativePath);
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

    public DataCollection DownloadRepositoryData(RepositoryFactory repositoryFactory)
    {
        // identity
        var identity = this.DownloadCurrentContextData(
            repositoryFactory.GetIdentityRepository()
        ).Data.Me;
        // sidebar
        var sidebar = this.DownloadSidebarData(
            repositoryFactory.GetSidebarRepository()
        );
        // child journeys
        var journeyRepository = repositoryFactory.GetLearningJourneyRepository();
        journeyRepository.Clear();
        var childJourneys = this.DownloadChildJourneyData(
            journeyRepository,
            sidebar.ChildProfileItems.Select(child => child.Id),
            variants: [
                "REGULAR_OBSERVATION",
                "PARENT_OBSERVATION",
                "ASSESSMENT",
                "TWO_YEAR_PROGRESS"
            ]
        ).ToList();
        // child summaries
        var summaries = this.DownloadChildSummaryData(
            repositoryFactory.GetChildSummaryRepository(),
            sidebar.ChildProfileItems.Select(item => item.Id)
        ).ToBlockingEnumerable().ToList();
        // feed items
        var feedItems = this.DownloadFeedItemData(
            repositoryFactory.GetFeedItemRepository()
        ).ToList();
        // observations
        var observationIds = feedItems
            .Where(feedItem => feedItem.Embed is FeedEmbedObservation)
            .Select(feedItem => ((FeedEmbedObservation)(feedItem.Embed ?? throw new InvalidOperationException())).ObservationId)
            .Union(
                childJourneys
                    .SelectMany(journey => journey.Data.ChildDevelopment.Observations.Results)
                    .Select(observation => observation.Id)
            );
        var observations = this.DownloadObservationData(
            repositoryFactory.GetObservationRepository(),
            observationIds
        ).ToList();
        // child notes
        var childNotes = this.DownloadChildNoteData(
            repositoryFactory.GetChildNoteRepository(),
            sidebar.ChildProfileItems.Select(child => child.Id)
        ).ToList();
        // check we've read an observation for all "observation" feed items
        var missingObservations = feedItems
            .Where(
                feedItem =>
                    (feedItem.Embed is FeedEmbedObservation) &&
                    (observations.SingleOrDefault(
                        observation => observation.Id == ((FeedEmbedObservation)(feedItem.Embed ?? throw new InvalidOperationException())).ObservationId
                    ) is null)
            ).ToList();
        if (missingObservations.Count > 0)
        {
            throw new InvalidOperationException();
        }
        // check we've got a child item for all sidebar child links
        var missingSummaries = sidebar.ChildProfileItems
            .Where(
                sidebarItem => summaries.SingleOrDefault(
                    summary => summary.Child.ChildId == sidebarItem.Id
                ) is null
            ).ToList();
        if (missingSummaries.Count > 0)
        {
            throw new InvalidOperationException();
        }
        var repository = new DataCollection(
            identity, sidebar, summaries, feedItems, observations, childNotes
        );
        return repository;
    }

    public void DownloadRepositoryContent(DataCollection repository)
    {
        this.DownloadChildNoteDataContent(repository.ChildNotes);
        this.DownloadFeedItemContent(repository.FeedItems);
        this.DownloadObservationContent(repository.Observations);
        this.DownloadSidebarContent(repository.Sidebar);
    }

    public async Task DownloadStaticResources(DataCollection repository)
    {
        // static resources
        await this.DownloadStaticFonts();
        await this.DownloadStaticImages();
    }

}
