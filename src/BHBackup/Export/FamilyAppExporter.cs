using BHBackup.Client.ApiV1.Feeds.Models;
using BHBackup.Helpers;
using BHBackup.Models;
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
        var learningJourney = this.DownloadLearningJourneyData();
        var identity = this.DownloadCurrentContextData().Data.Me;
        var sidebar = this.DownloadSidebarData();
        var summaries = this.DownloadChildSummaryData(sidebar).ToBlockingEnumerable().ToList();
        var feedItems = this.DownloadFeedItemData().ToList();
        var observations = this.DownloadObservationsData(
            feedItems
            .Where(feedItem => feedItem.Embed is FeedEmbedObservation)
            .Select(feedItem => ((FeedEmbedObservation)(feedItem.Embed ?? throw new InvalidOperationException())).ObservationId)
        ).ToList();
        var childNotes = this.DownloadChildNoteData(
            sidebar.ChildProfileItems.Select(child => child.Id)
        ).ToList();
        var repository = new FamilyAppRepository(
            identity, sidebar, summaries, feedItems, observations, childNotes
        );
        FamilyAppExporter.UpdateOfflineUrls(repository);
        return repository;
    }

    public FamilyAppRepository ReadRepositoryData()
    {
        var roundtrip = true;
        var identity = (this.ReadCurrentContextData(roundtrip) ?? throw new InvalidOperationException()).Data.Me;
        var sidebar = this.ReadSidebarData(roundtrip) ?? throw new InvalidOperationException();
        var summaries = this.ReadChildSummaryData(roundtrip);
        var feedItems = this.ReadFeedItemData(roundtrip).ToList();
        var observations = this.ReadObservationsData(roundtrip).ToList();
        var childNotes = this.ReadChildNoteData(roundtrip).ToList();
        var repository = new FamilyAppRepository(
            identity, sidebar, summaries, feedItems, observations, childNotes
        );
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

        var site = new Site(
            newsfeedPage: new(
                name: "newsfeed",
                templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.Pages.newsfeed-page.liquid",
                outputFilename: OfflinePathHelper.GetNewsfeedPageRelativePath(),
                title: "Bright Horizons | Newsfeed",
                topBar: new(
                    selectedIcon:"home",
                    title:"Newsfeed"
                )
            ),
            childNotesPages: repository.Sidebar.ChildProfileItems
                .Select(
                    sidebarItem => new ChildNotesPage(
                        name: $"childprofile-notes-{sidebarItem.Id}",
                        templateFilename: $"{typeof(Liquid.EmbeddedResources).Namespace}.Pages.childprofile-notes-page.liquid",
                        outputFilename: OfflinePathHelper.GetChildProfileNotesPageRelativePath(sidebarItem.Title),
                        title: "Bright Horizons | Child profile",
                        topBar: new(
                            selectedIcon: "home",
                            title: "Child profile"
                        ),
                        childSummary: repository.ChildSummaries.First(childSummary => childSummary.Child.ChildId == sidebarItem.Id)
                    )
                ).ToList()
        );

        var pages = new List<GenericPage>()
            .Union(new List<GenericPage> { index })
            .Union(new List<GenericPage> { site.NewsfeedPage })
            .Union(site.ChildNotesPages)
            .ToList();
        foreach (var page in pages)
        {
            this.RenderLiquidTemplate(
                site, page, repository
            );
        }

    }
    
}
