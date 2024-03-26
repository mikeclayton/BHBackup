using BHBackup.Client.ApiV1;
using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.ApiV2;
using BHBackup.Client.Core;
using BHBackup.Client.GraphQl;
using BHBackup.Common.Helpers;
using BHBackup.Download.Extensions;
using BHBackup.Storage;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHBackup.Download;

public sealed partial class ContentDownloader
{

    public ContentDownloader(ILogger logger, HttpClient httpClient, CoreApiCredentials apiCredentials, string outputDirectory, bool overwrite)
    {
        this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.ApiCredentials = apiCredentials ?? throw new ArgumentNullException(nameof(apiCredentials));
        this.OutputDirectory = outputDirectory ?? throw new ArgumentNullException(nameof(outputDirectory));
        this.Overwrite = overwrite;
    }

    public ILogger Logger
    {
        get;
    }

    public string OutputDirectory
    {
        get;
    }

    public HttpClient HttpClient
    {
        get;
    }

    public CoreApiCredentials ApiCredentials
    {
        get;
    }

    public bool Overwrite 
    {
        get;
    }

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
            this.Logger.LogInformation($"skipping '{relativePath}'...");
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
        this.Logger.LogInformation($"downloading '{relativePath}'...");
        var response = await this.HttpClient.SendAsync(request).ConfigureAwait(false);
        await using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        using var fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write);
        await responseStream.CopyToAsync(fileStream).ConfigureAwait(false);
    }

    public async Task<DataCollection> DownloadRepositoryData(RepositoryFactory repositoryFactory)
    {
        // identity
        var identity = await this.DownloadCurrentContextData(
            repositoryFactory.GetIdentityRepository()
        );
        // sidebar
        var sidebar = await this.DownloadSidebarData(
            repositoryFactory.GetSidebarRepository()
        );
        // child journeys
        var journeyRepository = repositoryFactory.GetLearningJourneyRepository();
        journeyRepository.Clear();
        var childJourneys = await this.DownloadChildJourneyData(
            journeyRepository,
            sidebar.ChildProfileItems.Select(child => child.Id),
            variants: [
                "REGULAR_OBSERVATION",
                "PARENT_OBSERVATION",
                "ASSESSMENT",
                "TWO_YEAR_PROGRESS"
            ]
        ).ConfigureAwait(false).ToListAsync();
        // child summaries
        var summaries = await this.DownloadChildSummaryData(
            repositoryFactory.GetChildSummaryRepository(),
            sidebar.ChildProfileItems.Select(item => item.Id)
        ).ConfigureAwait(false).ToListAsync();
        // feed items
        var feedItems = await this.DownloadFeedItemData(
            repositoryFactory.GetFeedItemRepository()
        ).ConfigureAwait(false).ToListAsync();
        // observations
        var observationIds = feedItems
            .Where(feedItem => feedItem.Embed is FeedEmbedObservation)
            .Select(feedItem => ((FeedEmbedObservation)(feedItem.Embed ?? throw new InvalidOperationException())).ObservationId)
            .Concat(
                childJourneys
                    .SelectMany(journey => journey.Data.ChildDevelopment.Observations.Results)
                    .Select(observation => observation.Id)
            ).Distinct();
        var observations = await this.DownloadObservationData(
            repositoryFactory.GetObservationRepository(),
            observationIds
        ).ConfigureAwait(false).ToListAsync();
        // child notes
        var childNotes = await this.DownloadChildNoteData(
            repositoryFactory.GetChildNoteRepository(),
            sidebar.ChildProfileItems.Select(child => child.Id)
        ).ConfigureAwait(false).ToListAsync();
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
            identity.Data.Me, sidebar, summaries, feedItems, observations, childNotes
        );
        return repository;
    }

    public async Task DownloadRepositoryContent(DataCollection repository)
    {
        await this.DownloadChildNoteContent(repository.ChildNotes).ConfigureAwait(false);
        await this.DownloadFeedItemContent(repository.FeedItems).ConfigureAwait(false);
        await this.DownloadObservationContent(repository.Observations).ConfigureAwait(false);
        await this.DownloadSidebarContent(repository.Sidebar).ConfigureAwait(false);
    }

    public async Task DownloadStaticResources(DataCollection repository)
    {
        // static resources
        await this.DownloadStaticFonts().ConfigureAwait(false);
        await this.DownloadStaticImages().ConfigureAwait(false);
    }

}
