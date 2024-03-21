using BHBackup.Client.ApiV1.Feeds;
using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.ApiV2.ChildSummary;
using BHBackup.Client.ApiV2.ChildSummary.Models;
using BHBackup.Client.ApiV2.Sidebar.Models;
using BHBackup.Client.GraphQl.ChildNotes;
using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Client.GraphQl.Identity;
using BHBackup.Client.GraphQl.Identity.Api;
using BHBackup.Client.GraphQl.LearningJourney;
using BHBackup.Client.GraphQl.LearningJourney.Models;
using BHBackup.Client.GraphQl.Observations;
using BHBackup.Client.GraphQl.Observations.Models;
using BHBackup.Storage;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    private IEnumerable<ChildNote> DownloadChildNoteData(ChildNoteRepository repository, IEnumerable<string> childIds)
    {
        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
        // read the child notes from the api
        Console.WriteLine("downloading child notes data...");
        var pageIndex = 1;
        var childNotes = graphQlClient.PaginateChildNotes(
                childId: childIds.First(),
                noteTypes: ["Classic"],
                limit: 10,
                parentVisible: true,
                safeguardingConcerns: false,
                sensitive: false,
                onBeforeRequest: () =>
                {
                    Console.WriteLine($"    downloading child notes data page {pageIndex}...");
                    pageIndex++;
                }
            ).ToBlockingEnumerable()
            .SelectMany(
                response => response.Data.ChildNotes.Result
            ).ToList();
        // save the child notes to disk in individual files
        foreach (var childNote in childNotes)
        {
            repository.WriteItem(childNote);
            yield return childNote;
        }
    }

    private async IAsyncEnumerable<ChildSummary> DownloadChildSummaryData(ChildSummaryRepository repository, IEnumerable<string> childIds)
    {
        var apiV2Client = this.DownloadHelper.GetApiV2Client();
        // save the child summaries to disk in individual files
        Console.WriteLine("downloading child summaries...");
        foreach (var childId in childIds)
        {
            var childSummary = await apiV2Client.GetChildSummary(childId);
            repository.WriteItem(childSummary);
            yield return childSummary;
        }
    }

    private IEnumerable<FeedItem> DownloadFeedItemData(FeedItemRepository repository)
    {
        var feedsClient = this.DownloadHelper.GetApiV1Client();
        // read the feed items from the api
        Console.WriteLine("downloading feed item data...");
        var feedItems = feedsClient.PaginateFeedItems(
                onBeforeRequest: timestamp =>
                    Console.WriteLine($"    downloading feed item data from {timestamp:yyyy-MM-dd}")
            ).ToBlockingEnumerable()
            .SelectMany(
                response => response.FeedItems
            ).ToList();
        // save the feed items to disk in individual files
        foreach (var feedItem in feedItems)
        {
            repository.WriteItem(feedItem);
            yield return feedItem;
        }
    }

    private GetCurrentContextResponse DownloadCurrentContextData(IdentityRepository repository)
    {
        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
        Console.WriteLine("downloading identity...");
        var currentContext = graphQlClient.GetCurrentContext().Result;
        // save the current context to disk
        repository.WriteItem(currentContext);
        return currentContext;
    }

    private IEnumerable<LearningJourneyQueryResponse> DownloadLearningJourneyData(
        LearningJourneyRepository repository, IEnumerable<string> childIds, IEnumerable<string> variants)
    {
        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
        // read the learning journey from the api
        Console.WriteLine("downloading learning journey data...");
        var counter = 1;
        foreach (var childId in childIds)
        {
            var journeys = graphQlClient.PaginateLearningJourneys(
                childId: childId, variants
            ).ToBlockingEnumerable().ToList();
            // save the journeys to disk in individual files
            foreach (var journey in journeys)
            {
                repository.WriteItem(journey, counter);
                yield return journey;
                counter++;
            }
        }
    }

    private IEnumerable<Observation> DownloadObservationData(ObservationRepository repository, IEnumerable<string> observationIds)
    {
        var graphQlClient = this.DownloadHelper.GetGraphQlClient();
        // read the observations from the api
        Console.WriteLine("downloading observation data...");
        var observations = graphQlClient.PaginateObservationsByIds(
                observationIds,
                pageSize: 5,
                onBeforeRequest: () =>
                    Console.WriteLine($"    downloading observation data page...")
            ).ToBlockingEnumerable()
            .SelectMany(response => response.Data.ChildDevelopment.Observations.Results)
            .ToList();
        // save the observations to disk in individual files
        foreach (var observation in observations)
        {
            repository.WriteItem(observation);
            yield return observation;
        }
    }

    private Sidebar DownloadSidebarData(SidebarRepository repository)
    {
        var apiV2Client = this.DownloadHelper.GetApiV2Client();
        Console.WriteLine("downloading sidebar...");
        var sidebar = apiV2Client.GetSidebar().Result;
        // save the context to disk
        repository.WriteItem(sidebar);
        return sidebar;
    }

}
