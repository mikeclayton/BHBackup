using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Client.GraphQl.Observations.Api;
using BHBackup.Helpers;
using BHBackup.Models;
using BHBackup.Storage;
using BHBackup.Visitors;

namespace BHBackup.Export;

internal sealed partial class FamilyAppExporter
{

    public FamilyAppExporter(DownloadHelper downloadHelper)
    {
        this.DownloadHelper = downloadHelper?? throw new ArgumentNullException(nameof(downloadHelper));
    }

    private DownloadHelper DownloadHelper
    {
        get;
    }

    public FamilyAppRepository DownloadRepositoryData()
    {
        //var learningJourney = this.DownloadLearningJourneyData();
        var identity = this.DownloadCurrentContextData(new IdentityRepository(this.DownloadHelper.RepositoryDirectory, true)).Data.Me;
        var sidebar = this.DownloadSidebarData(new SidebarRepository(this.DownloadHelper.RepositoryDirectory, true));
        var summaries = this.DownloadChildSummaryData(new ChildSummaryRepository(this.DownloadHelper.RepositoryDirectory, true), sidebar).ToBlockingEnumerable().ToList();
        var feedItems = this.DownloadFeedItemData(new FeedItemRepository(this.DownloadHelper.RepositoryDirectory, true)).ToList();
        var observations = this.DownloadObservationData(
            new ObservationRepository(this.DownloadHelper.RepositoryDirectory, true),
            feedItems
                .Where(feedItem => feedItem.Embed is FeedEmbedObservation)
                .Select(feedItem => ((FeedEmbedObservation)(feedItem.Embed ?? throw new InvalidOperationException())).ObservationId)
        ).ToList();
        var childNotes = this.DownloadChildNoteData(
            new ChildNoteRepository(this.DownloadHelper.RepositoryDirectory, true),
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
        var repository = new FamilyAppRepository(
            identity, sidebar, summaries, feedItems, observations, childNotes
        );
        FamilyAppExporter.UpdateOfflineUrls(repository);
        return repository;
    }

    public FamilyAppRepository ReadRepositoryData()
    {
        //var learningJourney = this.DownloadLearningJourneyData();
        var identity = new IdentityRepository(this.DownloadHelper.RepositoryDirectory, true).ReadItem().Data.Me;
        var sidebar = new SidebarRepository(this.DownloadHelper.RepositoryDirectory, true).ReadItem();
        var summaries = new ChildSummaryRepository(this.DownloadHelper.RepositoryDirectory, true).ReadAll().ToList();
        var feedItems = new FeedItemRepository(this.DownloadHelper.RepositoryDirectory, true).ReadAll().ToList();
        var observations = new ObservationRepository(this.DownloadHelper.RepositoryDirectory, true).ReadAll().ToList();
        var childNotes = new ChildNoteRepository(this.DownloadHelper.RepositoryDirectory, true).ReadAll().ToList();
        var repository = new FamilyAppRepository(
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

    private static void UpdateOfflineUrls(FamilyAppRepository repository)
    {
        var visitor = new OfflineUrlVisitor();
        visitor.Visit(repository);
    }

    public async Task DownloadResources(FamilyAppRepository repository)
    {
        var visitor = new DownloadVisitor(
            this.DownloadHelper
        );
        visitor.Visit(repository);
        // static resources
        await this.DownloadStaticHttpFonts();
        await this.DownloadStaticHttpImages();
    }

    public void GenerateHtmlFiles(FamilyAppRepository repository)
    {

        this.UnpackEmbeddedStylesheets(true);

        var children = repository.Observations
            .SelectMany(observation => observation.Children)
            .DistinctBy(child => child.Id)
            .ToList();

        var index = new GenericPage(
            name: "index",
            templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.Pages.index.liquid",
            outputFilename: "index.htm"
        );

        var pages = new List<GenericPage>()
            .Concat(
                // index
                new List<GenericPage> { index }
            ).Concat(
                // newsfeed
                new List<GenericPage> {
                    new NewsfeedPage(
                        name: "newsfeed",
                        templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.Pages.newsfeed-page.liquid",
                        outputFilename: OfflinePathHelper.GetNewsfeedPageRelativePath(),
                        title: "Bright Horizons | Newsfeed",
                        topBar: new(
                            style: "newsfeed",
                            title: "Newsfeed"
                        )
                    )
                }
            )
            //.Concat(
            //    // child profile - journey
            //    repository.Sidebar.ChildProfileItems
            //        .Select(
            //            sidebarItem => new ChildProfilePage(
            //                name: $"childprofile-journey-{sidebarItem.Id}",
            //                templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.Pages.childprofile-journey-page.liquid",
            //                outputFilename: OfflinePathHelper.GetChildProfileJourneyPageRelativePath(sidebarItem.Title, sidebarItem.Id),
            //                title: "Bright Horizons | Child profile",
            //                topBar: new(
            //                    style: "child-profile",
            //                    title: "Child profile"
            //                ),
            //                childSummary: repository.ChildSummaries.Single(childSummary => childSummary.Child.ChildId == sidebarItem.Id)
            //            )
            //        )
            //)
            .Concat(
                // child profile - notes
                repository.Sidebar.ChildProfileItems
                    .Select(
                        sidebarItem => new ChildProfilePage(
                            name: $"childprofile-notes-{sidebarItem.Id}",
                            templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.Pages.childprofile-notes-page.liquid",
                            outputFilename: OfflinePathHelper.GetChildProfileNotesPageRelativePath(sidebarItem.Title, sidebarItem.Id),
                            title: "Bright Horizons | Child profile",
                            topBar: new(
                                style: "child-profile",
                                title: "Child profile"
                            ),
                            childSummary: repository.ChildSummaries.Single(childSummary => childSummary.Child.ChildId == sidebarItem.Id)
                        )
                    )
            )
            .ToList();

        foreach (var page in pages)
        {
            this.RenderLiquidTemplate(
                page, repository
            );
        }

    }
    
}
