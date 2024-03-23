using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.ApiV2.ChildSummary.Models;
using BHBackup.Client.ApiV2.Sidebar.Models;
using BHBackup.Client.GraphQl.ChildNotes.Models;
using BHBackup.Client.GraphQl.Identity.Models.Context;
using BHBackup.Client.GraphQl.Observations.Models;
using System.Collections.ObjectModel;

namespace BHBackup.Storage;

public sealed class DataCollection
{

    public DataCollection(
        Me identity,
        Sidebar sidebar,
        IEnumerable<ChildSummary> childSummaries,
        IEnumerable<FeedItem> feedItems,
        IEnumerable<Observation> observations,
        IEnumerable<ChildNote> childNotes
    )
    {
        this.Identity = identity ?? throw new ArgumentNullException(nameof(identity));
        this.Sidebar = sidebar ?? throw new ArgumentNullException(nameof(sidebar));
        this.ChildSummaries = new(
            (childSummaries ?? throw new ArgumentNullException(nameof(childSummaries)))
                .ToList()
        );
        this.FeedItems = new(
            (feedItems ?? throw new ArgumentNullException(nameof(feedItems)))
                .ToList()
        );
        this.Observations = new(
            (observations ?? throw new ArgumentNullException(nameof(observations)))
                .ToList()
        );
        this.ChildNotes = new(
            (childNotes ?? throw new ArgumentNullException(nameof(childNotes)))
                .ToList()
        );
    }

    public Me Identity
    {
        get;
    }

    public Sidebar Sidebar
    {
        get;
    }

    public ReadOnlyCollection<ChildSummary> ChildSummaries
    {
        get;
    }

    public ReadOnlyCollection<FeedItem> FeedItems
    {
        get;
    }

    public ReadOnlyCollection<Observation> Observations
    {
        get;
    }

    public ReadOnlyCollection<ChildNote> ChildNotes
    {
        get;
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
        return repository;
    }

}
