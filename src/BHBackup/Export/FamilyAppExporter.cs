using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Download;
using BHBackup.Storage;
using BHBackup.Visitors;

namespace BHBackup.Export;

internal static  partial class FamilyAppExporter
{

    public static DataCollection DownloadRepositoryData(ContentDownloader downloader, RepositoryFactory repositoryFactory)
    {
        // identity
        var identity = downloader.DownloadCurrentContextData(
            repositoryFactory.GetIdentityRepository()
        ).Data.Me;
        // sidebar
        var sidebar = downloader.DownloadSidebarData(
            repositoryFactory.GetSidebarRepository()
        );
        // child journeys
        var journeyRepository = repositoryFactory.GetLearningJourneyRepository();
        journeyRepository.Clear();
        var childJourneys = downloader.DownloadLearningJourneyData(
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
        var summaries = downloader.DownloadChildSummaryData(
            repositoryFactory.GetChildSummaryRepository(),
            sidebar.ChildProfileItems.Select(item => item.Id)
        ).ToBlockingEnumerable().ToList();
        // feed items
        var feedItems = downloader.DownloadFeedItemData(
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
        var observations = downloader.DownloadObservationData(
            repositoryFactory.GetObservationRepository(),
            observationIds
        ).ToList();
        // child notes
        var childNotes = downloader.DownloadChildNoteData(
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
        FamilyAppExporter.UpdateOfflineUrls(repository);
        return repository;
    }

    public static DataCollection ReadRepositoryData(RepositoryFactory repositoryFactory)
    {
        //var learningJourney = this.DownloadLearningJourneyData();
        var identity = repositoryFactory.GetIdentityRepository().ReadItem().Data.Me;
        var sidebar = repositoryFactory.GetSidebarRepository().ReadItem();
        var summaries = repositoryFactory.GetChildSummaryRepository().ReadAll().ToList();
        var feedItems = repositoryFactory.GetFeedItemRepository().ReadAll().ToList();
        var observations = repositoryFactory.GetObservationRepository().ReadAll().ToList();
        var childNotes = repositoryFactory.GetChildNoteRepository().ReadAll().ToList();
        var repository = new DataCollection(
            identity, sidebar, summaries, feedItems, observations, childNotes
        );
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
        FamilyAppExporter.UpdateOfflineUrls(repository);
        return repository;
    }

    private static void UpdateOfflineUrls(DataCollection repository)
    {
        var visitor = new OfflineUrlVisitor();
        visitor.Visit(repository);
    }

    public async static Task DownloadStaticResources(ContentDownloader downloader, DataCollection repository)
    {
        var visitor = new DownloadVisitor(downloader);
        visitor.Visit(repository);
        // static resources
        await downloader.DownloadStaticHttpFonts();
        await downloader.DownloadStaticHttpImages();
    }

}
